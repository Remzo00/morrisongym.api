using System.Security.Claims;

namespace Morrison_Gym.API.Services.TokenService;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromToken(string token);
}