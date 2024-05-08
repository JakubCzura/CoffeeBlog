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
}