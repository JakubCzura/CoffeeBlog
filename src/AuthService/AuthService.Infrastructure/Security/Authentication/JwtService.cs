using AuthService.Application.Dtos.Authentication;
using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Domain.SettingsOptions.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Infrastructure.Security.Authentication;

/// <summary>
/// Security service to perform operations related to JWT and provide token creation.
/// </summary>
/// <param name="_authenticationOptions">Settings for authentication.</param>
/// <param name="dateTimeProvider">Interface to provide date and time.</param>
internal class JwtService(IOptions<AuthenticationOptions> _authenticationOptions,
                          IDateTimeProvider dateTimeProvider) : IJwtService
{
    private readonly AuthenticationOptions _authenticationOptions = _authenticationOptions.Value;

    public string CreateToken(CreateJwtTokenDto createJwtTokenUserDetailsDto,
                              IEnumerable<string>? roles = null,
                              IEnumerable<Claim>? claims = null)
    {
        ArgumentNullException.ThrowIfNull(createJwtTokenUserDetailsDto);

        List<Claim> tokenClaims =
        [
            new Claim(ClaimTypes.NameIdentifier, createJwtTokenUserDetailsDto.UserId.ToString()),
            new Claim(ClaimTypes.Name, createJwtTokenUserDetailsDto.Username),
            new Claim(ClaimTypes.Email, createJwtTokenUserDetailsDto.Email),
        ];

        if (roles?.Any() == true)
        {
            IEnumerable<Claim> tokenRoles = roles.Select(role => new Claim(ClaimTypes.Role, role));
            tokenClaims.AddRange(tokenRoles);
        }

        if (claims?.Any() == true)
        {
            tokenClaims.AddRange(claims);
        }

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_authenticationOptions.Jwt.SecretKey));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        DateTime expires = dateTimeProvider.UtcNow.AddMinutes(Convert.ToDouble(_authenticationOptions.Jwt.LifetimeInMinutes));

        JwtSecurityToken jwtSecurityToken = new(issuer: _authenticationOptions.Jwt.Issuer,
                                                audience: _authenticationOptions.Jwt.Audience,
                                                claims: tokenClaims,
                                                expires: expires,
                                                signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}