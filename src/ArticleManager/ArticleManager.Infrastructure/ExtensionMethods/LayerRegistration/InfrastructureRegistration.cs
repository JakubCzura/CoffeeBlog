using ArticleManager.Application.Interfaces.Helpers;
using ArticleManager.Application.Interfaces.Persistence.Repositories;
using ArticleManager.Infrastructure.ExtensionMethods.Authentication;
using ArticleManager.Infrastructure.ExtensionMethods.Database;
using ArticleManager.Infrastructure.Helpers;
using ArticleManager.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManager.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
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