using AuthService.Domain.Entities;
using AuthService.Domain.SettingsOptions.UserCredential;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UserLastPassword"/>.
/// </summary>
public interface IUserLastPasswordRepository : IBaseRepository<UserLastPassword>
{
    /// <summary>
    /// Returns all the last hashed passwords of user with specified id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>All the last hashed passwords of user with specified id.</returns>
    Task<List<UserLastPassword>> GetLastPasswordsByUserIdAsync(int userId,
                                                               CancellationToken cancellationToken);

    /// <summary>
    /// Adjust count of last hashed passwords of user with specified id.
    /// When user changes password, this method should be called to adjust the count of last hashed passwords.
    /// Count of last hashed passwords is specified in appsettings.json and can be read from <see cref="UserCredentialOptions.LastPasswordCount"/>.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="lastPasswordCount">Count of passwords to keep in database.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows deleted in database.</returns>
    Task<int> AdjustUserLastPasswordCountByUserIdAsync(int userId,
                                                       int lastPasswordCount,
                                                       CancellationToken cancellationToken);
}