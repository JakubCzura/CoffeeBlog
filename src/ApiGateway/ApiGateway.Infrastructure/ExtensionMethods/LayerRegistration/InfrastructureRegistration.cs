using ApiGateway.Infrastructure.ExtensionMethods.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of infrastructure layer services.
/// </summary>
public static class InfrastructureRegistration
{
    /// <summary>
    /// Registers infrastructure layer services.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    /// <returns>Instance of <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.ConfigureAuthentication();

        return services;
    }
}