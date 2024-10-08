﻿using AuthService.Application.Dtos.Accounts.Repository;
using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="Account"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
/// <param name="dateTimeProvider">Interface to provide date and time.</param>
internal class AccountRepository(AuthServiceDbContext _authServiceDbContext,
                                 IDateTimeProvider dateTimeProvider)
    : BaseRepository<Account>(_authServiceDbContext), IAccountRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<int> RemoveAccountsBansDueToExpirationAsync(CancellationToken cancellationToken = default)
    {
        DateOnly currentDate = dateTimeProvider.FromDateTime(dateTimeProvider.UtcNow);
        return await _authServiceDbContext.Accounts.Where(account => account.BanEndsAt != null && account.BanEndsAt < currentDate)
                                                   .ExecuteUpdateAsync(Account => Account.SetProperty(property => property.IsBanned, false)
                                                                                         .SetProperty(property => property.BanReason, (value) => null)
                                                                                         .SetProperty(property => property.BanNote, (value) => null)
                                                                                         .SetProperty(property => property.BannedAt, (value) => null)
                                                                                         .SetProperty(property => property.BanEndsAt, (value) => null), cancellationToken);
    }

    public async Task<int> BanAccountByUserIdAsync(BanAccountByUserIdDto banUserAccountByUserIdDto,
                                                   CancellationToken cancellationToken = default)
    {
        DateOnly currentDate = dateTimeProvider.FromDateTime(dateTimeProvider.UtcNow);
        return await _authServiceDbContext.Accounts.Where(account => account.UserId == banUserAccountByUserIdDto.UserId)
                                                   .ExecuteUpdateAsync(Account => Account.SetProperty(property => property.IsBanned, true)
                                                                                         .SetProperty(property => property.BanReason, banUserAccountByUserIdDto.BanReason)
                                                                                         .SetProperty(property => property.BanNote, banUserAccountByUserIdDto.BanNote)
                                                                                         .SetProperty(property => property.BannedAt, currentDate)
                                                                                         .SetProperty(property => property.BanEndsAt, banUserAccountByUserIdDto.BanEndsAt), cancellationToken);
    }

    public async Task<int> RemoveAccountBanByUserIdAsync(int userId,
                                                         CancellationToken cancellationToken = default)
       => await _authServiceDbContext.Accounts.Where(account => account.UserId == userId)
                                              .ExecuteUpdateAsync(Account => Account.SetProperty(property => property.IsBanned, false)
                                                                                    .SetProperty(property => property.BanReason, (value) => null)
                                                                                    .SetProperty(property => property.BanNote, (value) => null)
                                                                                    .SetProperty(property => property.BannedAt, (value) => null)
                                                                                    .SetProperty(property => property.BanEndsAt, (value) => null), cancellationToken);
}