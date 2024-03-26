using System.Security.Claims;
using Daily_Planner.Domain.Dto;
using Daily_Planner.Domain.Result;

namespace Daily_Planner.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}