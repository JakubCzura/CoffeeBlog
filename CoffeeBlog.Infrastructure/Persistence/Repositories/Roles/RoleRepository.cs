using CoffeeBlog.Application.Interfaces.Persistence.Repositories.Roles;
using CoffeeBlog.Domain.Entities.Roles;
using CoffeeBlog.Infrastructure.Persistence.Repositories.DbEntitiesBase;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories.Roles;

public class RoleRepository : DbEntityBaseRepository<Role>, IRoleRepository
{
}