using AuthService.Application.Interfaces.Helpers;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="UserDetail"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
/// <param name="_dateTimeProvider">Interface to provide date and time.</param>
internal class UserDetailRepository(AuthServiceDbContext _authServiceDbContext,
                                    IDateTimeProvider _dateTimeProvider)
    : DbEntityBaseRepository<UserDetail>(_authServiceDbContext), IUserDetailRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;
    private readonly IDateTimeProvider _dateTimeProvider = _dateTimeProvider;

    public async Task<int> UpdateLastSuccessfullSignInAsync(int userId,
                                                            CancellationToken cancellationToken = default)
       => await _authServiceDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                 .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastSuccessfullSignIn, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastFailedSignInAsync(int userId,
                                                       CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                  .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastFailedSignIn, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastUsernameChangeAsync(int userId,
                                                         CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                  .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastUsernameChange, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastEmailChangeAsync(int userId,
                                                      CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                  .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastEmailChange, _dateTimeProvider.UtcNow), cancellationToken);

    public async Task<int> UpdateLastPasswordChangeAsync(int userId,
                                                         CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserDetails.Where(userDetail => userDetail.UserId == userId)
                                                  .ExecuteUpdateAsync(userDetail => userDetail.SetProperty(property => property.LastPasswordChange, _dateTimeProvider.UtcNow), cancellationToken);
}