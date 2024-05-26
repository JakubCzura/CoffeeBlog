using AuthService.Application.Dtos.Accounts.Repository;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="Account"/>.
/// </summary>
public interface IAccountRepository : IDbEntityBaseRepository<Account>
{
    /// <summary>
    /// Removes all accounts bans that have expired.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> RemoveAccountsBansDueToExpirationAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Ban account of user.
    /// </summary>
    /// <param name="banUserAccountByUserIdDto">Details to ban user's account.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> BanAccountByUserIdAsync(BanAccountByUserIdDto banUserAccountByUserIdDto,
                                      CancellationToken cancellationToken);

    /// <summary>
    /// Removes account ban for user with specified id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> RemoveAccountBanByUserIdAsync(int userId,
                                            CancellationToken cancellationToken);
}