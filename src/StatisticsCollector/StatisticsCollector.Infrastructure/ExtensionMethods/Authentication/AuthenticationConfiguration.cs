using StatisticsCollector.Infrastructure.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.Authentication;

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