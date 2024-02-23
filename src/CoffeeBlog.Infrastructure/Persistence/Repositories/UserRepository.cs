using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

public class UserRepository(CoffeeBlogDbContext coffeeBlogDbContext) : DbEntityBaseRepository<UserEntity>(coffeeBlogDbContext), IUserRepository
{ }