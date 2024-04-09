using AuthService.Application.Dtos.Authentication;
using System.Security.Claims;

namespace AuthService.Application.Interfaces.Security.Authentication;

/// <summary>
/// Interface for JWT service to provide JWT token.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Create JWT token for a signed in user.
    /// </summary>
    /// <param name="createJwtTokenUserDetailsDto">Details with information which should be included in JWT token.</param>
    /// <param name="roles">User's roles for authorization.</param>
    /// <param name="claims">User's claims for authorization.</param>
    /// <returns></returns>
    public string CreateToken(CreateJwtTokenDto createJwtTokenUserDetailsDto,
                              IEnumerable<string>? roles = null,
                              IEnumerable<Claim>? claims = null);
}