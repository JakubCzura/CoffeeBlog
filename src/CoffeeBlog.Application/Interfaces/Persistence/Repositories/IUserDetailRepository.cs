using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="UserDetail"/>.
/// </summary>
public interface IUserDetailRepository : IDbEntityBaseRepository<UserDetail>
{
    /// <summary>
    /// Updates the date and time when user successfully signed in last time and sets it to current UTC datetime.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateLastSuccessfullSignInAsync(int userId,
                                               CancellationToken cancellationToken);
    /// <summary>
    /// Updates the date and time when user failed to sign in last time and sets it to current UTC datetime.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateLastLastFailedSignInAsync(int userId,
                                              CancellationToken cancellationToken);

    /// <summary>
    /// Updates the date and time when user changed username last time and sets it to current UTC datetime.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateLastUsernameChangeAsync(int userId,
                                            CancellationToken cancellationToken);

    /// <summary>
    /// Updates the date and time when user changed e-mail last time and sets it to current UTC datetime.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateLastEmailChangeAsync(int userId,
                                         CancellationToken cancellationToken);

    /// <summary>
    /// Updates the date and time when user changed password last time and sets it to current UTC datetime.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateLastPasswordChangeAsync(int userId,
                                            CancellationToken cancellationToken);
}