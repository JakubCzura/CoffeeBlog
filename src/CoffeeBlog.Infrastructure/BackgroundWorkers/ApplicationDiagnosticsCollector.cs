using Microsoft.Extensions.Logging;
using Quartz;

namespace AuthService.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class ApplicationDiagnosticsCollector(ILogger<ApplicationDiagnosticsCollector> _logger) : IJob
{
    private readonly ILogger<ApplicationDiagnosticsCollector> _logger = _logger;

    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(ApplicationDiagnosticsCollector)}: Exception while saving data to database");
            return Task.CompletedTask;
        }
    }
}