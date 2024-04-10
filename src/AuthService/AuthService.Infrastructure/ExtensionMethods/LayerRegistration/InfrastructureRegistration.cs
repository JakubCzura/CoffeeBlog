using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Domain.SettingsOptions.Authentication;
using AuthService.Domain.SettingsOptions.PasswordHasher;
using AuthService.Domain.SettingsOptions.UserCredential;
using AuthService.Infrastructure.ExtensionMethods.Authentication;
using AuthService.Infrastructure.ExtensionMethods.Database;
using AuthService.Infrastructure.Helpers;
using AuthService.Infrastructure.Persistence.Repositories;
using AuthService.Infrastructure.Security.Authentication;
using AuthService.Infrastructure.Security.CurrentUsers;
using AuthService.Infrastructure.Security.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
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

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();

        //services.ConfigureBackgroundWorkers(configuration);

        return services;
    }
}