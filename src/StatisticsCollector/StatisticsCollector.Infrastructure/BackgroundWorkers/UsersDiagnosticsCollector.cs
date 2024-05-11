using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using Microsoft.Extensions.Logging;
using Quartz;
using StatisticsCollector.Application.Interfaces.Helpers;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class UsersDiagnosticsCollector(ILogger<UsersDiagnosticsCollector> _logger,
                                       IUserDiagnosticRepository _userDiagnosticRepository,
                                       IDateTimeProvider _dateTimeProvider,
                                       IRequestPublisher<GetUserDiagnosticRequest> _requestPublisher) : IJob
{
    private readonly ILogger<UsersDiagnosticsCollector> _logger = _logger;
    private readonly IUserDiagnosticRepository _userDiagnosticRepository = _userDiagnosticRepository;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;
    private readonly IRequestPublisher<GetUserDiagnosticRequest> _requestPublisher = _requestPublisher;

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            GetUserDiagnosticRequest getUserDiagnosticRequest = new(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.AddDays(-1)),
                                                                    nameof(UsersDiagnosticsCollector));
            await _requestPublisher.GetResponseAsync<GetUserDiagnosticRequest, GetUserDiagnosticResponse>(getUserDiagnosticRequest, default);

            await _userDiagnosticRepository.CreateAsync(new UserDiagnostic(), default);
            throw new NotImplementedException("map response to entity");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception while saving data to database");
        }
    }
}