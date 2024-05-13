using EventBus.Domain.Responses.Basics;

namespace EventBus.Domain.Responses.AuthService.UserDiagnostics;

/// <summary>
/// Details with users' diagnostic information.
/// </summary>
/// <param name="NewUserCount">How many new accounts have been created.</param>
/// <param name="ActiveAccountCount">How many accounts are active.</param>
/// <param name="BannedAccountCount">How many accounts are banned.</param>
/// <param name="MostCommonBanReason">The most common reason to ban account.</param>
/// <param name="UserWhoLoggedInCount">How many users logged in.</param>
/// <param name="UserWhoFailedToLogInCount">How many users failed to log in.</param>
/// <param name="UserWhoChangedUsernameCount">How many users have changed their usernames.</param>
/// <param name="UserWhoChangedEmailCount">How many users have changed their e-mails.</param>
/// <param name="UserWhoChangedPasswordCount">How many users have changed their passwords.</param>
/// <param name="DataCollectedAt">Date that represents when the data was collected.</param>
/// <param name="RequestId">Id of request.</param>
/// <param name="ResponsePublisherName">Name of publisher that produces response for request.</param>
public record GetUsersDiagnosticDataResponse(int NewUserCount,
                                             int ActiveAccountCount,
                                             int BannedAccountCount,
                                             string MostCommonBanReason,
                                             int UserWhoLoggedInCount,
                                             int UserWhoFailedToLogInCount,
                                             int UserWhoChangedUsernameCount,
                                             int UserWhoChangedEmailCount,
                                             int UserWhoChangedPasswordCount,
                                             DateTime DataCollectedAt,
                                             Guid RequestId,
                                             string ResponsePublisherName) : AuthServiceResponseBase(RequestId, ResponsePublisherName);