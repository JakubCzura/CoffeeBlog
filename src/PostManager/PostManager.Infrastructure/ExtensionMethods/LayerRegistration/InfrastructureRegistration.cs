using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostManager.Infrastructure.ExtensionMethods.Database;

namespace PostManager.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        return services;
    }
}