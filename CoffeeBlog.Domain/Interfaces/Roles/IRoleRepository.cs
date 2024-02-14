using CoffeeBlog.Domain.Entities.Roles;
using CoffeeBlog.Domain.Interfaces.Base;

namespace CoffeeBlog.Domain.Interfaces.Roles;

public interface IRoleRepository : IDbEntityBaseRepository<Role>
{
}