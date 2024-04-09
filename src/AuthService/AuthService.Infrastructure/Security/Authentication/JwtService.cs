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

internal class JwtService(IOptions<AuthenticationOptions> authenticationOptions,
                          IDateTimeProvider dateTimeHelper) : IJwtService
{
    private readonly AuthenticationOptions _authenticationOptions = authenticationOptions.Value;
    private readonly IDateTimeProvider _dateTimeHelper = dateTimeHelper;

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
        DateTime expires = _dateTimeHelper.UtcNow.AddMinutes(Convert.ToDouble(_authenticationOptions.Jwt.LifetimeInMinutes));

        JwtSecurityToken jwtSecurityToken = new(issuer: _authenticationOptions.Jwt.Issuer,
                                                audience: _authenticationOptions.Jwt.Audience,
                                                claims: tokenClaims,
                                                expires: expires,
                                                signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}