using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Dto.User;
using Daily_Planner.Domain.Result;

namespace Daily_Planner.Domain.Interfaces.Services;

/// <summary>
/// Service for authorisation and registration
/// </summary>
public interface IAuthServices
{
    /// <summary>
    /// User registration
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    
    /// <summary>
    /// User authorisation
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}