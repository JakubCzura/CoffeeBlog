using CoffeeBlog.Application.Interfaces.Persistence.Repositories.DbEntitiesBase;
using CoffeeBlog.Domain.Entities.Roles;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories.Roles;

public interface IRoleRepository : IDbEntityBaseRepository<Role>
{
}