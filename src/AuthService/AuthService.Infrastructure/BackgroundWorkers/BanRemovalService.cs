using AuthService.Application.Interfaces.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AuthService.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class BanRemovalService(ILogger<BanRemovalService> _logger,
                               IAccountRepository _accountRepository) : IJob
{
    private readonly ILogger<BanRemovalService> _logger = _logger;
    private readonly IAccountRepository _accountRepository = _accountRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _accountRepository.RemoveAccountsBansDueToExpirationAsync(default);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(BanRemovalService)}: Exception while saving data to database");
        }
    }
}