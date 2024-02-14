using CoffeeBlog.Domain.Entities.Users;
using CoffeeBlog.Domain.Interfaces.Users;
using CoffeeBlog.Infrastructure.Repositories.Base;

namespace CoffeeBlog.Infrastructure.Repositories.Users;

public class UserRepository : DbEntityBaseRepository<User>, IUserRepository
{
}