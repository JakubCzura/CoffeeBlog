using EventBus.Domain.Responses.Basics;

namespace EventBus.Domain.Responses.AuthService.UserDiagnostics;

/// <param name="ActiveAccountCount">How many accounts are active.</param>
/// <param name="BannedAccountCount">How many accounts are banned.</param>
/// <param name="FailedSignInAttemptCount">How many failed attempts to log in happened.</param>
/// <param name="SuccessfulSignInAttemptCount">How many successful attempts to log in happened.</param>
/// <param name="DataCollectedAt"> Date that represents when the data was collected.
/// Preferred way to collect data is to use quartz next day to collect data for the previous day. </param>
public record GetUserDiagnosticResponse(int ActiveAccountCount,
                                        int BannedAccountCount,
                                        int FailedSignInAttemptCount,
                                        int SuccessfulSignInAttemptCount,
                                        DateOnly DataCollectedAt,
                                        Guid RequestId,
                                        string ResponsePublisherName) : AuthServiceResponseBase(RequestId, ResponsePublisherName);