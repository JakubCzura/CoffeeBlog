using CoffeeBlog.Domain.SettingsOptions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CoffeeBlog.Infrastructure.ExtensionMethods;

public static class AuthenticationConfiguration
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, AuthenticationOptions authenticationOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authenticationOptions.Jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authenticationOptions.Jwt.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Jwt.SecretKey)),
                        ValidateIssuerSigningKey = true
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = context =>
                        {
                            string? userId = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                            string? username = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                            string? email = context?.Principal?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                            //Avoid trying to authorize without valid user details in JWT
                            if (IsAnyClaimValueEmpty([userId, username, email]))
                            {
                                context?.Fail("Unauthorized. User's claims had invalid value");
                                return Task.CompletedTask;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        return services;
    }

    private static bool IsAnyClaimValueEmpty(IEnumerable<string?> claimsValues)
        => claimsValues.Any(string.IsNullOrWhiteSpace);
}