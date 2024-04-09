using AuthService.Infrastructure.Persistence.DatabaseContext;
using AuthService.Infrastructure.Persistence.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.ExtensionMethods.Database;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddDbContext<CoffeeBlogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogDbConnectionString"),
                                 sqlServerOptionsBuilder =>
                                 {
                                     sqlServerOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                     sqlServerOptionsBuilder.MigrationsAssembly(typeof(CoffeeBlogDbContext).Assembly.FullName);
                                 });
            options.UseTriggers(triggerOptions => triggerOptions.AddTrigger<AdjustUserLastPasswordCount>());
        });

        return services;
    }
}