using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Infrastructure.Email;
using NotificationProvider.Infrastructure.Factories.Emails;

namespace NotificationProvider.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.AddScoped<IEmailMessageFactory, EmailMessageFactory>();
        services.AddScoped<IEmailServiceProvider, EmailServiceProvider>();

        return services;
    }
}