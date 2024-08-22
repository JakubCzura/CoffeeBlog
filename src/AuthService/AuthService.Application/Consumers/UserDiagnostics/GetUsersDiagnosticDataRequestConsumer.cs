using AuthService.Application.Consumers.Basics;
using AuthService.Application.Dtos.UserDiagnostics;
using AuthService.Application.Interfaces.Persistence.Repositories;
using EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Consumers.UserDiagnostics;

/// <summary>
/// Consumer of <see cref="GetUsersDiagnosticDataRequest"/> request.
/// </summary>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="apiErrorRepository">Interface to perform api error operations in database.</param>
/// <param name="userDiagnosticDataRepository">Interface to perform user diagnostic data operations in database.</param>
public sealed class GetUsersDiagnosticDataRequestConsumer(ILogger<GetUsersDiagnosticDataRequestConsumer> logger,
                                                          IEventConsumerDetailRepository eventConsumerDetailRepository,
                                                          IApiErrorRepository apiErrorRepository,
                                                          IUserDiagnosticDataRepository userDiagnosticDataRepository)
    : EventConsumerBase<GetUsersDiagnosticDataRequest, GetUsersDiagnosticDataRequestConsumer>(logger, eventConsumerDetailRepository, apiErrorRepository)
{
    /// <summary>
    /// Consumes <see cref="GetUsersDiagnosticDataRequest"/> request.<br/>
    /// Sends diagnostics data about users as response.
    /// </summary>
    /// <param name="consumeContext">Request's context.</param>
    /// <returns><see cref="Task"/>.</returns>
    public override async Task ConsumeEvent(ConsumeContext<GetUsersDiagnosticDataRequest> consumeContext)
    {
        GetUsersDiagnosticDataResultDto userDiagnostic = await userDiagnosticDataRepository.GetUsersDiagnosticDataAsync(consumeContext.Message.DataCollectedAt, default);

        GetUsersDiagnosticDataResponse response = new(userDiagnostic.NewUserCount,
                                                      userDiagnostic.ActiveAccountCount,
                                                      userDiagnostic.BannedAccountCount,
                                                      userDiagnostic.MostCommonBanReason,
                                                      userDiagnostic.UserWhoLoggedInCount,
                                                      userDiagnostic.UserWhoFailedToLogInCount,
                                                      userDiagnostic.UserWhoChangedUsernameCount,
                                                      userDiagnostic.UserWhoChangedEmailCount,
                                                      userDiagnostic.UserWhoChangedPasswordCount,
                                                      consumeContext.Message.DataCollectedAt,
                                                      consumeContext.Message.EventId,
                                                      nameof(GetUsersDiagnosticDataRequestConsumer));

        await consumeContext.RespondAsync(response);
    }
}