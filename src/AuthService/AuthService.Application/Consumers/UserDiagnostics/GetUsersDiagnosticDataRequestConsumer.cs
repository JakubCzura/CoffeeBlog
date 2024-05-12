using AuthService.Application.Consumers.Basics;
using AuthService.Application.Dtos.UserDiagnostics;
using AuthService.Application.Interfaces.Persistence.Repositories;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using MassTransit;
using Microsoft.Extensions.Logging;

//Event consumer's namespace must be the same as event's namespace
namespace EventBus.Domain.Events.StatisticsCollector.UserDiagnostics;

public sealed class GetUsersDiagnosticDataRequestConsumer(ILogger<GetUsersDiagnosticDataRequestConsumer> _logger,
                                                          IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                          IApiErrorRepository _apiErrorRepository,
                                                          IUserDiagnosticDataRepository _userDiagnosticDataRepository )
    : EventConsumerBase<GetUsersDiagnosticDataRequest, GetUsersDiagnosticDataRequestConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IUserDiagnosticDataRepository _userDiagnosticDataRepository = _userDiagnosticDataRepository;

    public override async Task ConsumeEvent(ConsumeContext<GetUsersDiagnosticDataRequest> consumeContext)
    {
        GetUsersDiagnosticDataResultDto userDiagnostic = await _userDiagnosticDataRepository.GetUsersDiagnosticDataAsync(consumeContext.Message.DataCollectedAt, default);

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