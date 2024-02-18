using CoffeeBlog.Application.Interfaces.Authentication;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.SettingsOptions.Authentication;
using CoffeeBlog.Infrastructure.Authentication;
using CoffeeBlog.Infrastructure.Helpers;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using CoffeeBlog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.ExtensionMethods;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.AppsettingsKey));

        services.AddDbContext<CoffeeBlogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogDbConnectionString"),
                                 builer => builer.MigrationsAssembly(typeof(CoffeeBlogDbContext).Assembly.FullName));
        });

        services.AddScoped<IApiErrorRepository, ApiErrorRepository>();
        services.AddScoped<IRequestDetailRepository, RequestDetailRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.ConfigureAuthentication(configuration.GetSection(AuthenticationOptions.AppsettingsKey).Get<AuthenticationOptions>()!);

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}