using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

public interface IUserRepository : IDbEntityBaseRepository<UserEntity>
{
    /// <summary>
    /// Returns a user by email or username.
    /// </summary>
    /// <param name="usernameOrEmail">User's email or username.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>User if found, otherwise null.</returns>
    Task<UserEntity?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                CancellationToken cancellationToken);

    /// <summary>
    /// Checks if user with given e-mail or username exists.
    /// <para>Usernames and e-mails are unique in database. Moreover there must not be any username equal to any e-mail as it can cause many issues.</para>
    /// </summary>
    /// <param name="usernameOrEmail">User's e-mail or username.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>True if user with given username or e-mail exists.</returns>
    Task<bool> AreUsernameAndEmailUniqueAndDifferentAsync(string email,
                                                          string username,
                                                          CancellationToken cancellationToken);

}