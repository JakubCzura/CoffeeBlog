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
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="usersDiagnosticsRepository">Interface to perform users diagnostics operations in database.</param>
/// <param name="dateTimeProvider">Interface to provide date and time.</param>
/// <param name="requestPublisher">Interface to publish requests to get response from other microservices.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
[DisallowConcurrentExecution]
public class UsersDiagnosticsCollector(ILogger<UsersDiagnosticsCollector> logger,
                                       IUsersDiagnosticsRepository usersDiagnosticsRepository,
                                       IDateTimeProvider dateTimeProvider,
                                       IRequestPublisher<GetUsersDiagnosticDataRequest> requestPublisher,
                                       IMapper mapper) : IJob
{
    /// <summary>
    /// Collects diagnostics data about user's activities and saves it to database.
    /// </summary>
    /// <param name="context">Context of quartz job.</param>
    /// <returns><see cref="Task"/></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            DateTime dataCollectedAt = dateTimeProvider.UtcNow.AddDays(-1);
            GetUsersDiagnosticDataRequest getUserDiagnosticRequest = new(dataCollectedAt,
                                                                         nameof(UsersDiagnosticsCollector));

            Response<GetUsersDiagnosticDataResponse> response = await requestPublisher.GetResponseAsync<GetUsersDiagnosticDataResponse>(getUserDiagnosticRequest, default);

            UsersDiagnostics usersDiagnostics = mapper.Map<UsersDiagnostics>(response.Message);
            await usersDiagnosticsRepository.CreateAsync(usersDiagnostics, default);
        }
        catch (RequestFaultException exception)
        {
            logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception when receiving {nameof(GetUsersDiagnosticDataResponse)} response from {nameof(GetUsersDiagnosticDataRequest)} request");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception while saving data to database");
        }
    }
}