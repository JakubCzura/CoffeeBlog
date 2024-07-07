using Microsoft.Extensions.DependencyInjection;
using PostManager.Infrastructure.Security.Authentication;

namespace PostManager.Infrastructure.ExtensionMethods.Authentication;

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