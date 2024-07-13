using ApiGateway.Infrastructure.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure.ExtensionMethods.Authentication;

/// <summary>
/// Configuration of authentication services.
/// </summary>
public static class AuthenticationConfiguration
{
    /// <summary>
    /// Configures authentication services.
    /// </summary>
    /// <param name="services">Collection of dependency incjetion services.</param>
    /// <returns>Reference to dependency injection service collection.</returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication().AddJwtBearer();
        services.ConfigureOptions<JwtAuthenticationConfiguration>();
        services.ConfigureOptions<JwtValidationConfiguration>();

        return services;
    }
}