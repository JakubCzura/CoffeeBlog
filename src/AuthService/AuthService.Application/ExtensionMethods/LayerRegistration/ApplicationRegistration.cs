﻿using AuthService.Application.Behaviours.Validators;
using EventBus.API.ExtensionMethods.LayerRegistration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthService.Application.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of application layer services.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Instance of <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddApplicationDI(this IServiceCollection services,
                                                      IConfiguration configuration)
    {
        services.AddEventBus(configuration, Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();

        return services;
    }
}