using ArticleManager.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleManager.Infrastructure.ExtensionMethods.Database;

public static class DbContextConfiguration
{
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