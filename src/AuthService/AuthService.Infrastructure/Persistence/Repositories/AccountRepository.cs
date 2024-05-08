using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class AccountRepository(AuthServiceDbContext _authServiceDbContext,
                                     IDateTimeProvider _dateTimeProvider)
    : DbEntityBaseRepository<Account>(_authServiceDbContext), IAccountRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    public async Task<int> RemoveAccountsBansDueToExpirationAsync(CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserAccounts.Where(Account => Account.BanEndsAt != null && Account.BanEndsAt <= _dateTimeProvider.UtcNow)
                                                   .ExecuteUpdateAsync(Account => Account.SetProperty(property => property.IsBanned, false)
                                                                                                 .SetProperty(property => property.BanReason, (value) => null)
                                                                                                 .SetProperty(property => property.BanNote, (value) => null)
                                                                                                 .SetProperty(property => property.BannedAt, (value) => null)
                                                                                                 .SetProperty(property => property.BanEndsAt, (value) => null), cancellationToken);
}