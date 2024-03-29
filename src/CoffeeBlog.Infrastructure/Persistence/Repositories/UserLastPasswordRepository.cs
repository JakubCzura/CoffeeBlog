﻿using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.SettingsOptions.UserCredential;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class UserLastPasswordRepository(CoffeeBlogDbContext _coffeeBlogDbContext,
                                          IOptions<UserCredentialOptions> _userCredentialOptions) : DbEntityBaseRepository<UserLastPassword>(_coffeeBlogDbContext), IUserLastPasswordRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = _coffeeBlogDbContext;
    private readonly UserCredentialOptions _userCredentialOptions = _userCredentialOptions.Value;

    public async Task<List<UserLastPassword>> GetUserLastPasswordsAsync(int userId,
                                                                        CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserLastPasswords.AsNoTracking()
                                                       .Where(userLastPassword => userLastPassword.UserId == userId)
                                                       .ToListAsync(cancellationToken);

    public async Task<int> AdjustUserLastPasswordCountByUserIdAsync(int userId,
                                                                    CancellationToken cancellationToken) 
        => await _coffeeBlogDbContext.UserLastPasswords.Where(userLastPassword => userLastPassword.UserId == userId)
                                                       .OrderBy(userLastPassword => userLastPassword.CreatedAt)
                                                       .Skip(_userCredentialOptions.LastPasswordCount)
                                                       .ExecuteDeleteAsync(cancellationToken);
}