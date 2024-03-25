using System.Security.Claims;

namespace Daily_Planner.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshtoken();
}