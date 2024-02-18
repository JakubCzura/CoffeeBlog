using CoffeeBlog.Infrastructure.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.ExtensionMethods;

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