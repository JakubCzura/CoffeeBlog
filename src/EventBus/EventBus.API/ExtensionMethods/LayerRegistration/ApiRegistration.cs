using EventBus.API.ExtensionMethods.EventBus;
using EventBus.Domain.SettingsOptions.EventBus;
using EventBus.Infrastructure.ExtensionMethods.LayerRegistration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBus.API.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of API layer services.
/// </summary>
public static class ApiRegistration
{
    /// <summary>
    /// Registers API layer services and configures event bus. <br/><br/>
    /// <example>
    /// Recommended way to call this method is as follows:<br/>
    /// <code>
    /// services.AddEventBus(configuration, Assembly.GetExecutingAssembly());
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <param name="assembly">Assembly that contains the code that is currently executing.</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection AddEventBus(this IServiceCollection services,
                                                 IConfiguration configuration,
                                                 Assembly assembly)
    {
        services.Configure<EventBusOptions>(configuration.GetSection(EventBusOptions.AppsettingsKey));

        services.ConfigureEventBus(assembly);

        services.AddInfrastructureDI(configuration);

        return services;
    }
}