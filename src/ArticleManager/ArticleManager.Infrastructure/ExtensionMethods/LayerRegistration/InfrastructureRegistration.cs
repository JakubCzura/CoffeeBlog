using ArticleManager.Application.Interfaces.Helpers;
using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Infrastructure.ExtensionMethods.Authentication;
using ArticleManager.Infrastructure.ExtensionMethods.Database;
using ArticleManager.Infrastructure.Helpers;
using ArticleManager.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManager.Infrastructure.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of infrastructure layer servies.
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

        services.ConfigureAuthentication();

        return services;
    }
}