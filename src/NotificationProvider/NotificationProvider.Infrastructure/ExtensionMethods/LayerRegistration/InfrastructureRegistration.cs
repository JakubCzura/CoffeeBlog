using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Infrastructure.Email;
using NotificationProvider.Infrastructure.ExtensionMethods.Database;
using NotificationProvider.Infrastructure.Factories.Emails;
using NotificationProvider.Infrastructure.Persistence.Repositories;

namespace NotificationProvider.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IEmailMessageFactory, EmailMessageFactory>();
        services.AddScoped<IEmailServiceProvider, EmailServiceProvider>();

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEmailMessageDetailRepository, EmailMessageDetailRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();

        return services;
    }
}