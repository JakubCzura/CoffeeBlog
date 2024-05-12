namespace AuthService.Application.Dtos.UserDiagnostics;

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
public record GetUsersDiagnosticDataResultDto(int NewUserCount,
                                              int ActiveAccountCount,
                                              int BannedAccountCount,
                                              string MostCommonBanReason,
                                              int UserWhoLoggedInCount,
                                              int UserWhoFailedToLogInCount,
                                              int UserWhoChangedUsernameCount,
                                              int UserWhoChangedEmailCount,
                                              int UserWhoChangedPasswordCount);