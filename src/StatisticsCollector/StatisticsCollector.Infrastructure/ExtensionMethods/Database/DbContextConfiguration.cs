using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatisticsCollector.Domain.SettingsOptions.Database;
using StatisticsCollector.Infrastructure.Persistence.DatabaseContext;

namespace StatisticsCollector.Infrastructure.ExtensionMethods.Database;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
                                                    IConfiguration configuration)
    {
        DatabaseOptions databaseOptions = configuration.GetSection(DatabaseOptions.AppsettingsKey).Get<DatabaseOptions>()!;

        services.AddDbContext<StatisticsCollectorDbContext>(options =>
            options.UseMongoDB(databaseOptions.ConnectionString, databaseOptions.DatabaseName));

        return services;
    }
}