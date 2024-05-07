using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class UserAccountRepository(AuthServiceDbContext _authServiceDbContext,
                                     IDateTimeProvider _dateTimeProvider)
    : DbEntityBaseRepository<UserAccount>(_authServiceDbContext), IUserAccountRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    public async Task<int> RemoveAccountsBansDueToExpirationAsync(CancellationToken cancellationToken)
        => await _authServiceDbContext.UserAccounts.Where(userAccount => userAccount.BanEndsAt <= _dateTimeProvider.UtcNow)
                                                   .ExecuteUpdateAsync(userAccount => userAccount.SetProperty(property => property.IsBanned, false)
                                                                                                 .SetProperty(property => property.AccountBanReason, AccountBanReason.Unspecified)
                                                                                                 .SetProperty(property => property.BanNote, "")
                                                                                                 .SetProperty(property => property.BannedAt, _dateTimeProvider.UtcNow)
                                                                                                 .SetProperty(property => property.BanEndsAt, _dateTimeProvider.UtcNow), cancellationToken);
}