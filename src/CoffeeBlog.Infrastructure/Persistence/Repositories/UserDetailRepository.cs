using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.Repositories;

internal class UserDetailRepository(CoffeeBlogDbContext _coffeeBlogDbContext,
                                    IDateTimeProvider _dateTimeProvider) : DbEntityBaseRepository<UserDetail>(_coffeeBlogDbContext), IUserDetailRepository
{
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext = _coffeeBlogDbContext;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    public async Task<int> UpdateLastSuccessfullSignInAsync(int userId,
                                                            CancellationToken cancellationToken = default)
       => await _coffeeBlogDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastSuccessfullSignIn, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastFailedSignInAsync(int userId,
                                                           CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                 .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastFailedSignIn, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastUsernameChangeAsync(int userId,
                                                         CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                 .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastUsernameChange, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastEmailChangeAsync(int userId,
                                                      CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                 .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastEmailChange, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastPasswordChangeAsync(int userId,
                                                         CancellationToken cancellationToken = default)
        => await _coffeeBlogDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                 .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastPasswordChange, _dateTimeProvider.UtcNow), cancellationToken);
}