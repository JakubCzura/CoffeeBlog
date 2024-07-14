using EventBus.Domain.SettingsOptions.EventBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EventBus.API.ExtensionMethods.EventBus;

/// <summary>
/// Configuration of event bus.
/// </summary>
internal static class EventBusConfiguration
{
    /// <summary>
    /// Configures event bus that is used to communicate between microservices.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="assembly">Assembly of microservice that wants to use event bus.</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection ConfigureEventBus(this IServiceCollection services,
                                                       Assembly assembly)
    {
        services.AddMassTransit(massTransitConfig =>
        {
            massTransitConfig.SetKebabCaseEndpointNameFormatter();

            massTransitConfig.AddConsumers(assembly);

            massTransitConfig.UsingRabbitMq((context, busFactoryConfig) =>
            {
                EventBusOptions options = context.GetRequiredService<IOptions<EventBusOptions>>().Value;

                busFactoryConfig.Host(new Uri(options.Host), host =>
                {
                    host.Username(options.Username);
                    host.Password(options.Password);
                });

                busFactoryConfig.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}