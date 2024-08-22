using AuthService.Application.Dtos.Users.Repository;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="User"/>.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Returns true if user exists in database.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>True if user exists in database, otherwise false.</returns>
    Task<bool> UserExistsAsync(int userId,
                               CancellationToken cancellationToken);

    /// <summary>
    /// Returns a user by email or username.
    /// </summary>
    /// <param name="usernameOrEmail">User's email or username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>User if found, otherwise null.</returns>
    Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail,
                                          CancellationToken cancellationToken);

    /// <summary>
    /// Checks if given username exists in database.
    /// </summary>
    /// <param name="username">User's username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>True if username exists in database, otherwise false.</returns>
    Task<bool> UsernameExistsAsync(string username,
                                   CancellationToken cancellationToken);

    /// <summary>
    /// Checks if given e-mail exists in database.
    /// </summary>
    /// <param name="email">User's e-mail.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>True if e-mail exists in database, otherwise false.</returns>
    Task<bool> EmailExistsAsync(string email,
                                CancellationToken cancellationToken);

    /// <summary>
    /// Updates user's username in database.
    /// </summary>
    /// <param name="id">User's id.</param>
    /// <param name="username">User's new username.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateUsernameAsync(int id,
                                  string username,
                                  CancellationToken cancellationToken);

    /// <summary>
    /// Updates user's email in database.
    /// </summary>
    /// <param name="id">User's id.</param>
    /// <param name="email">User's new email.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateEmailAsync(int id,
                               string email,
                               CancellationToken cancellationToken);

    /// <summary>
    /// Updates user's password in database.
    /// </summary>
    /// <param name="id">User's id.</param>
    /// <param name="password">User's new password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdatePasswordAsync(int id,
                                  string password,
                                  CancellationToken cancellationToken);

    /// <summary>
    /// Updates user's forgotten password reset token and its expiration date in database.
    /// </summary>
    /// <param name="updateForgottenPasswordResetTokenDto">Details to update forgotten password reset token and its expiration date.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> UpdateForgottenPasswordResetTokenAsync(UpdateForgottenPasswordResetTokenDto updateForgottenPasswordResetTokenDto,
                                                     CancellationToken cancellationToken);

    /// <summary>
    /// Updates user's forgotten password reset token and its expiration date in database.
    /// </summary>
    /// <param name="id">User's id.</param>
    /// <param name="password">User's new password.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Total number of rows updated in database.</returns>
    Task<int> ResetForgottenPasswordAsync(int id,
                                          string password,
                                          CancellationToken cancellationToken);
}