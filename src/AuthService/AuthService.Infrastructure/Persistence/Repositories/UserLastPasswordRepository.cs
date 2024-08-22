using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="UserLastPassword"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
internal class UserLastPasswordRepository(AuthServiceDbContext _authServiceDbContext)
    : BaseRepository<UserLastPassword>(_authServiceDbContext), IUserLastPasswordRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<List<UserLastPassword>> GetLastPasswordsByUserIdAsync(int userId,
                                                                            CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserLastPasswords.AsNoTracking()
                                                        .Where(userLastPassword => userLastPassword.UserId == userId)
                                                        .ToListAsync(cancellationToken);

    public async Task<int> AdjustUserLastPasswordCountByUserIdAsync(int userId,
                                                                    int lastPasswordCount,
                                                                    CancellationToken cancellationToken)
        => await _authServiceDbContext.UserLastPasswords.Where(userLastPassword => userLastPassword.UserId == userId)
                                                        .OrderBy(userLastPassword => userLastPassword.CreatedAt)
                                                        .Skip(lastPasswordCount)
                                                        .ExecuteDeleteAsync(cancellationToken);
}