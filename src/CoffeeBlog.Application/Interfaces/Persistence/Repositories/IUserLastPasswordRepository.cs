using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UserLastPassword"/>.
/// </summary>
public interface IUserLastPasswordRepository : IDbEntityBaseRepository<UserLastPassword>
{
    /// <summary>
    /// Returns all the last hashed passwords of user with specified id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>All the last hashed passwords of user with specified id.</returns>
    Task<List<UserLastPassword>> GetUserLastPasswordsAsync(int userId,
                                                           CancellationToken cancellationToken);
}