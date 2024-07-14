using AutoMapper;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using StatisticsCollector.Application.Interfaces.Helpers;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.BackgroundWorkers;

/// <summary>
/// Background service for collecting diagnostics data about user's activities.
/// </summary>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_usersDiagnosticsRepository">Interface to perform users diagnostics operations in database.</param>
/// <param name="_dateTimeProvider">Interface to provide date and time.</param>
/// <param name="_requestPublisher">Interface to publish requests to get response from other microservices.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
[DisallowConcurrentExecution]
public class UsersDiagnosticsCollector(ILogger<UsersDiagnosticsCollector> _logger,
                                       IUsersDiagnosticsRepository _usersDiagnosticsRepository,
                                       IDateTimeProvider _dateTimeProvider,
                                       IRequestPublisher<GetUsersDiagnosticDataRequest> _requestPublisher,
                                       IMapper _mapper) : IJob
{
    private readonly ILogger<UsersDiagnosticsCollector> _logger = _logger;
    private readonly IUsersDiagnosticsRepository _usersDiagnosticsRepository = _usersDiagnosticsRepository;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;
    private readonly IRequestPublisher<GetUsersDiagnosticDataRequest> _requestPublisher = _requestPublisher;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Collects diagnostics data about user's activities and saves it to database.
    /// </summary>
    /// <param name="context">Context of quartz job.</param>
    /// <returns><see cref="Task"/></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            DateTime dataCollectedAt = _dateTimeProvider.UtcNow.AddDays(-1);
            GetUsersDiagnosticDataRequest getUserDiagnosticRequest = new(dataCollectedAt,
                                                                         nameof(UsersDiagnosticsCollector));

            Response<GetUsersDiagnosticDataResponse> response = await _requestPublisher.GetResponseAsync<GetUsersDiagnosticDataResponse>(getUserDiagnosticRequest, default);

            UsersDiagnostics usersDiagnostics = _mapper.Map<UsersDiagnostics>(response.Message);
            await _usersDiagnosticsRepository.CreateAsync(usersDiagnostics, default);
        }
        catch (RequestFaultException exception)
        {
            _logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception when receiving {nameof(GetUsersDiagnosticDataResponse)} response from {nameof(GetUsersDiagnosticDataRequest)} request");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception while saving data to database");
        }
    }
}