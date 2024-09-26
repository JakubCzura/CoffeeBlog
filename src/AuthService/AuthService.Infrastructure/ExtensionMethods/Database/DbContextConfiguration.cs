using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using AuthService.Infrastructure.Persistence.Triggers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.ExtensionMethods.Database;

/// <summary>
/// Configuration of database context.
/// </summary>
public static class DbContextConfiguration
{
    /// <summary>
    /// Configures database context.
    /// </summary>
    /// <param name="services">Collection of dependency injection services.</param>
    /// <param name="configuration">Appsettings.json</param>
    /// <returns>Reference to <paramref name="services"/></returns>
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddIdentityCore<User>(options =>
        {
            //options.User.RequireUniqueEmail = true;
            //options.Password.RequiredLength = 8;
            //options.Password.RequireDigit = true;
            //options.Password.RequireLowercase = true;
            //options.Password.RequireUppercase = true;
            //options.Password.RequireNonAlphanumeric = true;
            //options.Lockout.MaxFailedAccessAttempts = 5;
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        })
        .AddRoles<IdentityRole<int>>()
        .AddSignInManager<User>()
        .AddEntityFrameworkStores<AuthServiceDbContext>();

        services.AddDbContext<AuthServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogAuthServiceDbConnectionString"),
                                 sqlServerOptionsBuilder =>
                                 {
                                     sqlServerOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                     sqlServerOptionsBuilder.MigrationsAssembly(typeof(AuthServiceDbContext).Assembly.FullName);
                                 });
            options.UseTriggers(triggerOptions => triggerOptions.AddTrigger<AdjustUserLastPasswordCountTrigger>());
        });

        return services;
    }
}