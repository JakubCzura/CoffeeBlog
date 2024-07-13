using ArticleManager.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManager.Infrastructure.ExtensionMethods.Database;

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
        services.AddDbContext<ArticleManagerDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogArticleManagerDbConnectionString"),
                                 sqlServerOptionsBuilder =>
                                 {
                                     sqlServerOptionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                     sqlServerOptionsBuilder.MigrationsAssembly(typeof(ArticleManagerDbContext).Assembly.FullName);
                                 });
        });

        return services;
    }
}