using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Daily_Planner.Application.Resources;
using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Dto.User;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Enum;
using Daily_Planner.Domain.Interfaces.Repositories;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace Daily_Planner.Application.Services;

public class AuthService : IAuthServices
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper, 
        IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
    }

    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }

        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists
                };
            }

            var hashUserPassword = HashPassword(dto.Password);
            user = new User()
            {
                Login = dto.Login,
                Password = hashUserPassword
            };

            await _userRepository.CreateAsync(user);
            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<UserDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user == null)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            var isVerifyPassword = IsVerifyPassword(user.Password, dto.Password);
            if (!isVerifyPassword)
            {
                return new BaseResult<TokenDto>
                {
                    ErrorMessage = ErrorMessage.WrongUserPassword,
                    ErrorCode = (int)ErrorCodes.WrongUserPassword
                };
            }

            var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, "User Role")
            };
            var accesToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshtoken();
            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpirytime = DateTime.UtcNow.AddDays(7);
                };
                await _userTokenRepository.CreateAsync(userToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpirytime = DateTime.UtcNow.AddDays(7);
            }

            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accesToken,
                    RefreshToken = refreshToken
                }
            };
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };
        }
    }

    private bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPassword(userPassword);
        return hash == userPasswordHash;
    }

    private string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(bytes).ToLower();
    }
}