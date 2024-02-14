using CoffeeBlog.Domain.Entities.Users;
using CoffeeBlog.Domain.Interfaces.Base;

namespace CoffeeBlog.Domain.Interfaces.Users;

public interface IUserDetailRepository : IDbEntityBaseRepository<UserDetail>
{
}