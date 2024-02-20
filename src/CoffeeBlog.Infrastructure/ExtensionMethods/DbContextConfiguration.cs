using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.ExtensionMethods;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, 
                                                        IConfiguration configuration)
    {
        services.AddDbContext<CoffeeBlogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CoffeeBlogDbConnectionString"),
                                 builer => builer.MigrationsAssembly(typeof(CoffeeBlogDbContext).Assembly.FullName));
        });

        return services;
    }
}