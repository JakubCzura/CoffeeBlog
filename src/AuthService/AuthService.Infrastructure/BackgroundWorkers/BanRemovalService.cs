using AuthService.Application.Interfaces.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace AuthService.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class BanRemovalService(ILogger<BanRemovalService> _logger,
                               IUserAccountRepository _userAccountRepository) : IJob
{
    private readonly ILogger<BanRemovalService> _logger = _logger;
    private readonly IUserAccountRepository _userAccountRepository = _userAccountRepository;
  
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _userAccountRepository.RemoveAccountsBansDueToExpirationAsync(default);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(BanRemovalService)}: Exception while saving data to database");
        }
    }
}