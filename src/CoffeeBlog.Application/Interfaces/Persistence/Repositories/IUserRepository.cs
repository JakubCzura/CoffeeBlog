using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

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
    /// Checks if given email and username are unique in database and they are not equal.
    /// <para>Usernames and e-mails are unique in database. Moreover there must not be any username equal to any e-mail as it can cause many issues.
    /// That's why email are compared with username in the query.</para>
    /// <para>If this method returns false then new user must not be created in database.</para>
    /// </summary>
    /// <param name="username">User's username.</param>
    /// <param name="email">User's e-mail.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True if email and username are unique and different, otherwise false.</returns>
    Task<bool> AreUsernameAndEmailUniqueAndDifferentAsync(string username,
                                                          string email,
                                                          CancellationToken cancellationToken);

}