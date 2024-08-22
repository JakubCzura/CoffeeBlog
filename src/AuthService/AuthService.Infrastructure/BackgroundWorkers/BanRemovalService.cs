using AuthService.Application.Interfaces.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AuthService.Infrastructure.BackgroundWorkers;

/// <summary>
/// Background service for removing expired bans from users' accounts.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_accountRepository">Interface to perform account operations in database.</param>
[DisallowConcurrentExecution]
public class BanRemovalService(ILogger<BanRemovalService> logger,
                               IAccountRepository accountRepository) : IJob
{
    /// <summary>
    /// Removes expired bans from users' accounts.
    /// </summary>
    /// <param name="context">Context of quartz job.</param>
    /// <returns><see cref="Task"/></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await accountRepository.RemoveAccountsBansDueToExpirationAsync(default);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"{nameof(BanRemovalService)}: Exception while saving data to database");
        }
    }
}