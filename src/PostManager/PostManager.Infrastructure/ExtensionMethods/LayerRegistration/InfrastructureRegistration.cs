using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostManager.Application.Interfaces.Helpers;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Infrastructure.ExtensionMethods.Database;
using PostManager.Infrastructure.Helpers;
using PostManager.Infrastructure.Persistence.Repositories;

namespace PostManager.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();

        return services;
    }
}