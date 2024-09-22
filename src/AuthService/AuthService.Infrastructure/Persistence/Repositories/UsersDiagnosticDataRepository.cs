using AuthService.Application.Dtos.UserDiagnostics;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.AuthService.Enums;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to user diagnostic data.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
internal class UsersDiagnosticDataRepository(AuthServiceDbContext _authServiceDbContext) : IUserDiagnosticDataRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<GetUsersDiagnosticDataResultDto> GetUsersDiagnosticDataAsync(DateTime dataCollectedAt,
                                                                                   CancellationToken cancellationToken = default)
    {
        int newUserCount = await _authServiceDbContext.Users.AsNoTracking()
                                                            .CountAsync(user => user.CreatedAt.Date == dataCollectedAt.Date, cancellationToken);

        int activeAccountCount = await _authServiceDbContext.Accounts.AsNoTracking()
                                                                     .CountAsync(account => !account.IsBanned, cancellationToken);

        int bannedAccountCount = await _authServiceDbContext.Accounts.AsNoTracking()
                                                                     .CountAsync(userDetail => userDetail.IsBanned, cancellationToken);

        AccountBanReason? mostCommonBanReason = await _authServiceDbContext.Accounts.AsNoTracking()
                                                                                    .Where(account => account.BanReason != null)
                                                                                    .Select(account => account.BanReason)
                                                                                    .GroupBy(banReason => banReason)
                                                                                    .OrderByDescending(grouping => grouping.Count())
                                                                                    .Select(grouping => grouping.Key)
                                                                                    .FirstOrDefaultAsync(cancellationToken);

        int userWhoLoggedInCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                          .CountAsync(userDetail => userDetail.LastSuccessfullSignIn == dataCollectedAt.Date, cancellationToken);

        int userWhoFailedToLogInCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                               .CountAsync(userDetail => userDetail.LastFailedSignIn != null
                                                                                                      && userDetail.LastFailedSignIn.Value == dataCollectedAt.Date, cancellationToken);

        int userWhoChangedUsernameCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                                 .CountAsync(userDetail => userDetail.LastUsernameChange != null
                                                                                                        && userDetail.LastUsernameChange.Value == dataCollectedAt.Date, cancellationToken);

        int userWhoChangedEmailCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                              .CountAsync(userDetail => userDetail.LastEmailChange != null
                                                                                                     && userDetail.LastEmailChange.Value == dataCollectedAt.Date, cancellationToken);
        int userWhoChangedPasswordCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                                 .CountAsync(userDetail => userDetail.LastPasswordChange != null
                                                                                                        && userDetail.LastPasswordChange.Value == dataCollectedAt.Date, cancellationToken);

        GetUsersDiagnosticDataResultDto getUserDiagnosticResultDto = new(newUserCount,
                                                                         activeAccountCount,
                                                                         bannedAccountCount,
                                                                         (mostCommonBanReason ?? AccountBanReason.Other).ToString(),
                                                                         userWhoLoggedInCount,
                                                                         userWhoFailedToLogInCount,
                                                                         userWhoChangedUsernameCount,
                                                                         userWhoChangedEmailCount,
                                                                         userWhoChangedPasswordCount);
        return getUserDiagnosticResultDto;
    }
}