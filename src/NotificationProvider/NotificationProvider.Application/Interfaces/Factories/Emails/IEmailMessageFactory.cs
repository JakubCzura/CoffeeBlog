using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Interfaces.Factories.Emails;

/// <summary>
/// Interface for creating e-mail messages.
/// </summary>
public interface IEmailMessageFactory
{
    /// <summary>
    /// Creates an e-mail message to welcome new user.
    /// </summary>
    /// <param name="to">Email's recipient.</param>
    /// <param name="username">Recipient's username.</param>
    /// <returns>Message to send.</returns>
    IEmailMessage CreateWelcomeEmailMessage(string to,
                                            string username);

    /// <summary>
    /// Creates an e-mail message with information to reset user's password.
    /// </summary>
    /// <param name="to">Email's recipient.</param>
    /// <param name="username">Receipient's username.</param>
    /// <param name="token">Token to reset password.</param>
    /// <param name="expirationDate">Expiration date of password reset token.</param>
    /// <returns>Message to send.</returns>
    IEmailMessage CreatePasswordResetEmailMessage(string to,
                                                  string username,
                                                  string token,
                                                  DateTime expirationDate);
}