using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class ApiErrorRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<ApiError>(coffeeBlogDbContext), IApiErrorRepository
{
}