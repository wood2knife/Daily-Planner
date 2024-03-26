using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Daily_Planner.Application.Resources;
using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Enum;
using Daily_Planner.Domain.Interfaces.Repositories;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Daily_Planner.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Daily_Planner.Application.Services;

public class TokenService : ITokenService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly string _jwtKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IOptions<JwtSettings> options, IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
        _jwtKey = options.Value.JwtKey;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
    }
    
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(_issuer, _audience, claims,
                null, DateTime.UtcNow.AddMinutes(10), credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string GenerateRefreshToken()
    {
        var randomNumbers = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumbers);
        return Convert.ToBase64String(randomNumbers);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidateLifetime = true, 
            ValidAudience = _audience,
            ValidIssuer = _issuer
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new SecurityTokenException(ErrorMessage.InvalidToken);
        }
        return claimsPrincipal;
    }

    public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto)
    {
        string accessToken = dto.AccessToken;
        string refreshToken = dto.RefreshToken;
        var claimPrincipal = GetPrincipalFromExpiredToken(accessToken);
        var userName = claimPrincipal.Identity?.Name;

        var user = await _userRepository.GetAll()
            .Include(x => x.UserToken)
            .FirstOrDefaultAsync(x => x.Login == userName);

        if (user == null || user.UserToken.RefreshToken != refreshToken ||
            user.UserToken.RefreshTokenExpirytime <= DateTime.UtcNow)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = ErrorMessage.InvalidClientRequest
            };
        }

        var newAccessToken = GenerateAccessToken(claimPrincipal.Claims);
        var newRefreshToken = GenerateRefreshToken();
        
        user.UserToken.RefreshToken = newRefreshToken;
        await _userRepository.UpdateAsync(user);

        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }
        };
    }
}