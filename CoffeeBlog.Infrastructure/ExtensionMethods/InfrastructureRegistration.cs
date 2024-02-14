using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Errors;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Requests;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Roles;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Users;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using CoffeeBlog.Infrastructure.Persistence.Repositories.Errors;
using CoffeeBlog.Infrastructure.Persistence.Repositories.Requests;
using CoffeeBlog.Infrastructure.Persistence.Repositories.Roles;
using CoffeeBlog.Infrastructure.Persistence.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBlog.Infrastructure.ExtensionMethods;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                               IConfiguration configuration)
    {
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

        return services;
    }
}