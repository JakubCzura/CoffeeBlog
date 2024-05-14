using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatisticsCollector.Application.Interfaces.Helpers;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.SettingsOptions.Database;
using StatisticsCollector.Infrastructure.ExtensionMethods.BackgroundWorkers;
using StatisticsCollector.Infrastructure.ExtensionMethods.Database;
using StatisticsCollector.Infrastructure.Helpers;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;
using StatisticsCollector.Infrastructure.Persistence.Repositories;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();
        services.AddScoped<IRequestDetailRepository, RequestDetailRepository>();
        services.AddScoped<IUsersDiagnosticsRepository, UsersDiagnosticsRepository>();

        services.ConfigureBackgroundWorkers(configuration);

        return services;
    }
}