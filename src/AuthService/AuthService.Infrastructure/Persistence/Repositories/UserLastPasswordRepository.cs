using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.SettingsOptions.UserCredential;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure.Persistence.Repositories;

internal class UserLastPasswordRepository(AuthServiceDbContext _authServiceDbContext,
                                          IOptions<UserCredentialOptions> _userCredentialOptions) : DbEntityBaseRepository<UserLastPassword>(_authServiceDbContext), IUserLastPasswordRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;
    private readonly UserCredentialOptions _userCredentialOptions = _userCredentialOptions.Value;

    public async Task<List<UserLastPassword>> GetUserLastPasswordsAsync(int userId,
                                                                        CancellationToken cancellationToken = default)
        => await _authServiceDbContext.UserLastPasswords.AsNoTracking()
                                                       .Where(userLastPassword => userLastPassword.UserId == userId)
                                                       .ToListAsync(cancellationToken);

    public async Task<int> AdjustUserLastPasswordCountByUserIdAsync(int userId,
                                                                    CancellationToken cancellationToken) 
        => await _authServiceDbContext.UserLastPasswords.Where(userLastPassword => userLastPassword.UserId == userId)
                                                       .OrderBy(userLastPassword => userLastPassword.CreatedAt)
                                                       .Skip(_userCredentialOptions.LastPasswordCount)
                                                       .ExecuteDeleteAsync(cancellationToken);
}