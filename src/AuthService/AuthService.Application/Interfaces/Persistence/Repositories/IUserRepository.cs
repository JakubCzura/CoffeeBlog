using AuthService.Application.Dtos.Users.Repository;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="User"/>.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Updates user's forgotten password reset token and its expiration date in database.
    /// </summary>
    /// <param name="updateForgottenPasswordResetTokenDto">Details to update forgotten password reset token and its expiration date.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateForgottenPasswordResetTokenAsync(UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto,
                                                     CancellationToken cancellationToken);

}