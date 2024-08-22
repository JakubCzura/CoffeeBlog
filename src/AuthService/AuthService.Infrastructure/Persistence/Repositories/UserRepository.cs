using AuthService.Application.Dtos.Users.Repository;
using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository to perform database operations related to <see cref="User"/>.
/// </summary>
/// <param name="_authServiceDbContext">Database context.</param>
internal class UserRepository(AuthServiceDbContext _authServiceDbContext)
    : BaseRepository<User>(_authServiceDbContext), IUserRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;

    public async Task<bool> UserExistsAsync(int userId,
                                            CancellationToken cancellationToken)
        => await _authServiceDbContext.Users.AsNoTracking()
                                            .AnyAsync(user => user.Id == userId, cancellationToken);

    public async Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                                       CancellationToken cancellationToken)
        => await _authServiceDbContext.Users.AsNoTracking()
                                            .FirstOrDefaultAsync(user => user.Email == usernameOrEmail || user.Username == usernameOrEmail, cancellationToken);

    public async Task<bool> UsernameExistsAsync(string username,
                                                CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.AsNoTracking()
                                            .AnyAsync(user => user.Username == username, cancellationToken);

    public async Task<bool> EmailExistsAsync(string email,
                                             CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.AsNoTracking()
                                            .AnyAsync(user => user.Email == email, cancellationToken);

    public async Task<int> UpdateUsernameAsync(int id,
                                               string username,
                                               CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Id == id)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.Username, username), cancellationToken);

    public async Task<int> UpdateEmailAsync(int id,
                                            string email,
                                            CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Id == id)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.Email, email), cancellationToken);

    public async Task<int> UpdatePasswordAsync(int id,
                                               string password,
                                               CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Id == id)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.Password, password), cancellationToken);

    public async Task<int> UpdateForgottenPasswordResetTokenAsync(UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto,
                                                                  CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Email == updateForgottenPasswordResetTokenDto.UserEmail)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.ForgottenPasswordResetToken, updateForgottenPasswordResetTokenDto.ForgottenPasswordResetToken)
                                                                            .SetProperty(property => property.ForgottenPasswordResetTokenExpiresAt, updateForgottenPasswordResetTokenDto.ForgottenPasswordResetTokenExpiresAt), cancellationToken);

    public async Task<int> ResetForgottenPasswordAsync(int id,
                                                       string password,
                                                       CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Id == id)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.Password, password)
                                                                            .SetProperty(property => property.ForgottenPasswordResetToken, (value) => null)
                                                                            .SetProperty(property => property.ForgottenPasswordResetTokenExpiresAt, (value) => null), cancellationToken);
}