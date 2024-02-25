using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoffeeBlog.Application.ExtensionMethods;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services,
                                                      IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Don't perform auto validation to have more control over the validation process.
        //Prefer injection IValidator<T> via constructor and use it explicitly.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); 

        return services;
    }
}