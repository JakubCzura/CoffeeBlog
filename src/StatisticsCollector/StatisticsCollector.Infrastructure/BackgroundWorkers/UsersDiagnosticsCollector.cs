using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;
using Quartz;
using StatisticsCollector.Application.Interfaces.Helpers;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.BackgroundWorkers;

[DisallowConcurrentExecution]
public class UsersDiagnosticsCollector(ILogger<UsersDiagnosticsCollector> _logger,
                                       IUsersDiagnosticsRepository _usersDiagnosticsRepository,
                                       IDateTimeProvider _dateTimeProvider,
                                       IRequestPublisher<GetUsersDiagnosticDataRequest> _requestPublisher) : IJob
{
    private readonly ILogger<UsersDiagnosticsCollector> _logger = _logger;
    private readonly IUsersDiagnosticsRepository _usersDiagnosticsRepository = _usersDiagnosticsRepository;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;
    private readonly IRequestPublisher<GetUsersDiagnosticDataRequest> _requestPublisher = _requestPublisher;

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            DateTime dataCollectedAt = _dateTimeProvider.UtcNow.AddDays(-1);

            GetUsersDiagnosticDataRequest getUserDiagnosticRequest = new(dataCollectedAt,
                                                                         nameof(UsersDiagnosticsCollector));

            Response<GetUsersDiagnosticDataResponse> response = await _requestPublisher.GetResponseAsync<GetUsersDiagnosticDataResponse>(getUserDiagnosticRequest, default);

            UsersDiagnostics usersDiagnostics = new()
            {
                NewUserCount = response.Message.NewUserCount,
                ActiveAccountCount = response.Message.ActiveAccountCount,
                BannedAccountCount = response.Message.BannedAccountCount,
                MostCommonBanReason = response.Message.MostCommonBanReason,
                UserWhoLoggedInCount = response.Message.UserWhoLoggedInCount,
                UserWhoFailedToLogInCount = response.Message.UserWhoFailedToLogInCount,
                UserWhoChangedUsernameCount = response.Message.UserWhoChangedUsernameCount,
                UserWhoChangedEmailCount = response.Message.UserWhoChangedEmailCount,
                UserWhoChangedPasswordCount = response.Message.UserWhoChangedPasswordCount,
                DataCollectedAt = response.Message.DataCollectedAt
            };
            await _usersDiagnosticsRepository.CreateAsync(usersDiagnostics, default);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"{nameof(UsersDiagnosticsCollector)}: Exception while saving data to database");
        }
    }
}