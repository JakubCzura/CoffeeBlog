using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Repository for <see cref="User"/> for database operations.
/// </summary>
public interface IUserRepository : IDbEntityBaseRepository<User>
{
    /// <summary>
    /// Returns a user by email or username.
    /// </summary>
    /// <param name="usernameOrEmail">User's email or username.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User if found, otherwise null.</returns>
    Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                CancellationToken cancellationToken);

    /// <summary>
    /// Checks if given username exists in database.
    /// </summary>
    /// <param name="username">User's username.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True if username exists in database, otherwise false.</returns>
    Task<bool> UsernameExistsAsync(string username,
                                   CancellationToken cancellationToken);

    /// <summary>
    /// Checks if given e-mail exists in database.
    /// </summary>
    /// <param name="email">User's e-mail.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True if e-mail exists in database, otherwise false.</returns>
    Task<bool> EmailExistsAsync(string email,
                                CancellationToken cancellationToken);

}