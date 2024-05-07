using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.SettingsOptions.Database;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;
using StatisticsCollector.Infrastructure.Persistence.Repositories;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        DatabaseOptions databaseOptions = configuration.GetSection(DatabaseOptions.AppsettingsKey).Get<DatabaseOptions>()!;

        services.AddDbContext<StatisticsCollectorDbContext>(options =>
            options.UseMongoDB(databaseOptions.ConnectionString, databaseOptions.DatabaseName));

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();
        services.AddScoped<IRequestDetailRepository, RequestDetailRepository>();

        return services;
    }
}