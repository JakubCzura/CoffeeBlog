﻿using Microsoft.Extensions.DependencyInjection;
using NotificationProvider.Infrastructure.Security.Authentication;

namespace NotificationProvider.Infrastructure.ExtensionMethods.Authentication;

/// <summary>
/// Configuration of authentication services.
/// </summary>
public static class AuthenticationConfiguration
{
    /// <summary>
    /// Configures authentication services.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication().AddJwtBearer();
        services.ConfigureOptions<JwtAuthenticationConfiguration>();
        services.ConfigureOptions<JwtValidationConfiguration>();

        return services;
    }
}