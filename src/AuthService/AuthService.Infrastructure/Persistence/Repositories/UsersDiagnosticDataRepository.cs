using AuthService.Application.Dtos.UserDiagnostics;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Enums;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class UsersDiagnosticDataRepository(AuthServiceDbContext _authServiceDbContext) : IUserDiagnosticDataRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<GetUsersDiagnosticDataResultDto> GetUsersDiagnosticDataAsync(DateOnly dataCollectedAt,
                                                                                   CancellationToken cancellationToken = default)
    {
        //Due EF Core and possible issue with time convertion, it is necessary to convert time here, not in query
        DateTime dataCollectedTime = dataCollectedAt.ToDateTime(default).Date;

        int newUserCount = await _authServiceDbContext.Users.AsNoTracking()
                                                            .CountAsync(user => user.CreatedAt.Date == dataCollectedTime, cancellationToken);

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
                                                                          .CountAsync(userDetail => userDetail.LastSuccessfullSignIn == dataCollectedTime, cancellationToken);

        int userWhoFailedToLogInCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                               .CountAsync(userDetail => userDetail.LastFailedSignIn != null
                                                                                                      && userDetail.LastFailedSignIn.Value == dataCollectedTime, cancellationToken);

        int userWhoChangedUsernameCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                                 .CountAsync(userDetail => userDetail.LastUsernameChange != null
                                                                                                        && userDetail.LastUsernameChange.Value == dataCollectedTime, cancellationToken);

        int userWhoChangedEmailCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                              .CountAsync(userDetail => userDetail.LastEmailChange != null
                                                                                                     && userDetail.LastEmailChange.Value == dataCollectedTime, cancellationToken);
        int userWhoChangedPasswordCount = await _authServiceDbContext.UserDetails.AsNoTracking()
                                                                                 .CountAsync(userDetail => userDetail.LastPasswordChange != null
                                                                                                        && userDetail.LastPasswordChange.Value == dataCollectedTime, cancellationToken);

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