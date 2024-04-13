using EventBus.Domain.SettingsOptions.EventBus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace EventBus.API.ExtensionMethods.EventBus;

internal static class EventBusConfiguration
{
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