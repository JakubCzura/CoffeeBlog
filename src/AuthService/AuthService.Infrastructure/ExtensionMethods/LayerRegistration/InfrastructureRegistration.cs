﻿using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.Authentication;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Application.Interfaces.Security.Password;
using AuthService.Application.Interfaces.Security.Token;
using AuthService.Infrastructure.ExtensionMethods.Authentication;
using AuthService.Infrastructure.ExtensionMethods.BackgroundWorkers;
using AuthService.Infrastructure.ExtensionMethods.Database;
using AuthService.Infrastructure.Helpers;
using AuthService.Infrastructure.Persistence.Repositories;
using AuthService.Infrastructure.Security.Authentication;
using AuthService.Infrastructure.Security.CurrentUsers;
using AuthService.Infrastructure.Security.Password;
using AuthService.Infrastructure.Security.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.ExtensionMethods.LayerRegistration;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IEventConsumerDetailRepository, EventConsumerDetailRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<IUserDiagnosticDataRepository, UsersDiagnosticDataRepository>();
        services.AddScoped<IUserLastPasswordRepository, UserLastPasswordRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.ConfigureAuthentication();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ISecurityTokenGenerator, SecurityTokenGenerator>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();

        services.ConfigureBackgroundWorkers(configuration);

        return services;
    }
}