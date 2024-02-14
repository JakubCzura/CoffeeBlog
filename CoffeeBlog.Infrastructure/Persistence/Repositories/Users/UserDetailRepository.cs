using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Users;
using CoffeeBlog.Domain.Entities.Users;
using CoffeeBlog.Infrastructure.Persistence.Repositories.DbEntitiesBase;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories.Users;

public class UserDetailRepository : DbEntityBaseRepository<UserDetail>, IUserDetailRepository
{
}