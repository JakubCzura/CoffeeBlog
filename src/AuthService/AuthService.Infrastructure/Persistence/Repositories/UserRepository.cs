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
internal class UserRepository(AuthServiceDbContext _authServiceDbContext) : IUserRepository
{
    private readonly AuthServiceDbContext _authServiceDbContext = _authServiceDbContext;


    public async Task<int> UpdateForgottenPasswordResetTokenAsync(UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto,
                                                                  CancellationToken cancellationToken = default)
        => await _authServiceDbContext.Users.Where(user => user.Email == updateForgottenPasswordResetTokenDto.UserEmail)
                                            .ExecuteUpdateAsync(user => user.SetProperty(property => property.ForgottenPasswordResetToken, updateForgottenPasswordResetTokenDto.ForgottenPasswordResetToken)
                                                                            .SetProperty(property => property.ForgottenPasswordResetTokenExpiresAt, updateForgottenPasswordResetTokenDto.ForgottenPasswordResetTokenExpiresAt), cancellationToken);

  
}