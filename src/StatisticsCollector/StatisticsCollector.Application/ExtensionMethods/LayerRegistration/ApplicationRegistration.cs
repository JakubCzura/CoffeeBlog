using EventBus.API.ExtensionMethods.LayerRegistration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StatisticsCollector.Application.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of application layer services.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection AddApplicationDI(this IServiceCollection services,
                                                      IConfiguration configuration)
    {
        services.AddEventBus(configuration, Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}