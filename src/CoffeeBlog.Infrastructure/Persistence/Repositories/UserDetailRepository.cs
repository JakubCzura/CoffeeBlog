using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class UserDetailRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<UserDetailEntity>(coffeeBlogDbContext), IUserDetailRepository
{
}