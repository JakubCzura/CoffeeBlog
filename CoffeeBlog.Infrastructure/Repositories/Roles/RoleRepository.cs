using CoffeeBlog.Domain.Entities.Roles;
using CoffeeBlog.Domain.Interfaces.Roles;
using CoffeeBlog.Infrastructure.Repositories.Base;

namespace CoffeeBlog.Infrastructure.Repositories.Roles;

public class RoleRepository : DbEntityBaseRepository<Role>, IRoleRepository
{
}