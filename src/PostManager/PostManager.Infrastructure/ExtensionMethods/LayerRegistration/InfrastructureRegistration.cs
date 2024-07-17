using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostManager.Application.Interfaces.Helpers;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Infrastructure.ExtensionMethods.Authentication;
using PostManager.Infrastructure.ExtensionMethods.Database;
using PostManager.Infrastructure.Helpers;
using PostManager.Infrastructure.Persistence.Repositories;

namespace PostManager.Infrastructure.ExtensionMethods.LayerRegistration;

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
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostCommentRepository, PostCommentRepository>();

        services.ConfigureAuthentication();

        return services;
    }
}