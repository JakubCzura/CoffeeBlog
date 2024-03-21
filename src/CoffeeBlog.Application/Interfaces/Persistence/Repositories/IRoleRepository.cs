using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="Role"/>.
/// </summary>
public interface IRoleRepository : IDbEntityBaseRepository<Role>
{
    /// <summary>
    /// Returns all roles that are assigned to the user with given id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>User's all roles or empty collection if user with given id doesn't exist in database.</returns>
    Task<IEnumerable<Role>> GetAllByUserId(int userId,
                                           CancellationToken cancellationToken);
}