using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostManager.Infrastructure.Persistence.DatabaseContext;

namespace PostManager.Infrastructure.ExtensionMethods.Database;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddDbContext<PostManagerDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogPostManagerDbConnectionString"),
                                 sqlServerOptionsBuilder =>
                                 {
                                     sqlServerOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                     sqlServerOptionsBuilder.MigrationsAssembly(typeof(PostManagerDbContext).Assembly.FullName);
                                 });
        });

        return services;
    }
}