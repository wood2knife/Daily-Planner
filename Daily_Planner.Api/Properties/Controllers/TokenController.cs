using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Daily_Planner.Api.Properties.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;
    
    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await _tokenService.RefreshToken(tokenDto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}