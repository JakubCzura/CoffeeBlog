using ArticleManager.Application.Interfaces.Helpers;
using ArticleManager.Infrastructure.ExtensionMethods.Database;
using ArticleManager.Infrastructure.Helpers;
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

        return services;
    }
}