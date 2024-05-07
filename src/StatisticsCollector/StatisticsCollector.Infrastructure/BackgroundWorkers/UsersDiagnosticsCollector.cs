using Microsoft.Extensions.Logging;
using Quartz;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class UsersDiagnosticsCollector(ILogger<UsersDiagnosticsCollector> _logger,
                                       IUserDiagnosticRepository _userDiagnosticRepository) : IJob
{
    private readonly ILogger<UsersDiagnosticsCollector> _logger = _logger;
    private readonly IUserDiagnosticRepository _userDiagnosticRepository = _userDiagnosticRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _userDiagnosticRepository.CreateAsync(new UserDiagnostic(), default);
            throw new NotImplementedException();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception while saving data to database");         
        }
    }
}