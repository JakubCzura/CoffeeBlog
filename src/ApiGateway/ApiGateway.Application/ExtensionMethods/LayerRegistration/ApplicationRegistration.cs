using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Application.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of application layer services.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    /// <returns>Instance of <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        return services;
    }
}