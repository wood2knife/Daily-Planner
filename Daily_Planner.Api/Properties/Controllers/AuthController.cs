using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Dto.User;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Daily_Planner.Api.Properties.Controllers;

[ApiController]
public class AuthController : Controller
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }

    /// <summary>
    /// User registration
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
    {
        var response = await _authServices.Register(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    /// <summary>
    /// User authorisation
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
    {
        var response = await _authServices.Login(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}