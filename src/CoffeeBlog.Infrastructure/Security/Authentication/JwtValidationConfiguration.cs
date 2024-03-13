using CoffeeBlog.Domain.SettingsOptions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using CoffeeBlog.Application.ExtensionMethods.Collections;

namespace CoffeeBlog.Infrastructure.Security.Authentication;

internal sealed class JwtValidationConfiguration(IOptions<AuthenticationOptions> authenticationOptions) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions = authenticationOptions.Value;

    public void Configure(string? name, JwtBearerOptions options)
        => Configure(options);

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = _authenticationOptions.Jwt.ValidateIssuer,
            ValidIssuer = _authenticationOptions.Jwt.Issuer,
            ValidateAudience = _authenticationOptions.Jwt.ValidateAudience,
            ValidAudience = _authenticationOptions.Jwt.Audience,
            ValidateLifetime = _authenticationOptions.Jwt.ValidateLifetime,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Jwt.SecretKey)),
            ValidateIssuerSigningKey = _authenticationOptions.Jwt.ValidateIssuerSigningKey
        };
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = context =>
            {
                string? userId = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                string? username = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                string? email = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                //Don't authorize user without valid user's details in JWT
                if (new string?[] { userId, username, email }.IsAnyElementNullOrWhiteSpace())
                {
                    context?.Fail("Unauthorized. User's claims had invalid value");
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
        };
    }
}