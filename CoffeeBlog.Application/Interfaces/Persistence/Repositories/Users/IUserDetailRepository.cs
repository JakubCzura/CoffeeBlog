using CoffeeBlog.Application.Interfaces.Persistence.Repositories.DbEntitiesBase;
using CoffeeBlog.Domain.Entities.Users;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories.Users;

public interface IUserDetailRepository : IDbEntityBaseRepository<UserDetail>
{
}