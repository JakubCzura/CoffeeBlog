using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationProvider.Domain.SettingsOptions.Database;
using NotificationProvider.Infrastructure.Persistence.DatabaseContext;

namespace NotificationProvider.Infrastructure.ExtensionMethods.Database;

/// <summary>
/// Configuration of database context.
/// </summary>
public static class DbContextConfiguration
{
    /// <summary>
    /// Configures database context.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        DatabaseOptions databaseOptions = configuration.GetSection(DatabaseOptions.AppsettingsKey)
                                                       .Get<DatabaseOptions>()!;

        services.AddDbContext<NotificationProviderDbContext>(options =>
            options.UseMongoDB(databaseOptions.ConnectionString, databaseOptions.DatabaseName));

        return services;
    }
}