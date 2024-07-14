using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatisticsCollector.Application.Interfaces.Helpers;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Infrastructure.ExtensionMethods.Authentication;
using StatisticsCollector.Infrastructure.ExtensionMethods.BackgroundWorkers;
using StatisticsCollector.Infrastructure.ExtensionMethods.Database;
using StatisticsCollector.Infrastructure.Helpers;
using StatisticsCollector.Infrastructure.Persistence.Repositories;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of infrastructure layer services.
/// </summary>
public static class InfrastructureRegistration
{
    /// <summary>
    /// Registers infrastructure layer services.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();
        services.AddScoped<IRequestDetailRepository, RequestDetailRepository>();
        services.AddScoped<IUsersDiagnosticsRepository, UsersDiagnosticsRepository>();

        services.ConfigureAuthentication();

        services.ConfigureBackgroundWorkers(configuration);

        return services;
    }
}