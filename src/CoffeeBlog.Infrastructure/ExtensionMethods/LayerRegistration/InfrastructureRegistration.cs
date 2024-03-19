using CoffeeBlog.Application.Email;
using CoffeeBlog.Application.Factories.Emails;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.Authentication;
using CoffeeBlog.Application.Interfaces.Security.CurrentUsers;
using CoffeeBlog.Application.Interfaces.Security.Password;
using CoffeeBlog.Domain.SettingsOptions.Authentication;
using CoffeeBlog.Domain.SettingsOptions.PasswordHasher;
using CoffeeBlog.Infrastructure.Email;
using CoffeeBlog.Infrastructure.ExtensionMethods.Authentication;
using CoffeeBlog.Infrastructure.ExtensionMethods.Database;
using CoffeeBlog.Infrastructure.Factories.Emails;
using CoffeeBlog.Infrastructure.Helpers;
using CoffeeBlog.Infrastructure.Persistence.Repositories;
using CoffeeBlog.Infrastructure.Security.Authentication;
using CoffeeBlog.Infrastructure.Security.CurrentUsers;
using CoffeeBlog.Infrastructure.Security.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.AppsettingsKey));
        services.Configure<PasswordHasherOptions>(configuration.GetSection(PasswordHasherOptions.AppsettingsKey));

        services.ConfigureDbContext(configuration);

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IRequestDetailRepository, RequestDetailRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<IUserLastPasswordRepository, UserLastPasswordRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.ConfigureAuthentication();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IEmailMessageFactory, EmailMessageFactory>();
        services.AddScoped<IEmailServiceProvider, EmailServiceProvider>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();

        return services;
    }
}