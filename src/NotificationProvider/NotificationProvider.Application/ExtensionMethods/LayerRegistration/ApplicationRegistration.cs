﻿using EventBus.API.ExtensionMethods.LayerRegistration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationProvider.Application.Behaviours.Validators;
using NotificationProvider.Domain.Constants;
using Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;
using System.Reflection;

namespace NotificationProvider.Application.ExtensionMethods.LayerRegistration;

/// <summary>
/// Registration of application layer services.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
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
        services.AddValidatorsFromAssemblyContaining(typeof(SubscribeNewsletterCommandValidator));
        services.AddFluentValidationAutoValidation(config =>
        {
            config.Filter = type => ExcludeUnexpectedTypesFromFluentValidationAutoValidation(type);
        });
        return services;
    }

    private static Func<Type, bool> ExcludeUnexpectedTypesFromFluentValidationAutoValidation =>
        type => !FluentValidationConstants.TypesExcludedFromAutoValidation.Contains(type);
}