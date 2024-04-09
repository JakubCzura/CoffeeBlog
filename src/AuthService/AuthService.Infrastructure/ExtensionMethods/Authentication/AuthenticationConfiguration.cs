using AuthService.Infrastructure.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.ExtensionMethods.Authentication;

public static class AuthenticationConfiguration
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication().AddJwtBearer();
        services.ConfigureOptions<JwtAuthenticationConfiguration>();
        services.ConfigureOptions<JwtValidationConfiguration>();

        return services;
    }
}