using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="Role"/>.
/// </summary>
public interface IRoleRepository : IDbEntityBaseRepository<Role>
{
}