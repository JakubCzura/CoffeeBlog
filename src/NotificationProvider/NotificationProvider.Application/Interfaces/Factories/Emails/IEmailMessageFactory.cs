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
    /// <param name="recipientName">User's name.</param>
    /// <param name="recipientEmail">User's e-mail.</param>
    /// <returns>Message to send.</returns>
    IEmailMessage CreateWelcomeEmailMessage(string recipientName,
                                            string recipientEmail);

    /// <summary>
    /// Creates an e-mail message with information to reset user's password.
    /// </summary>
    /// <param name="recipientName">User's name.</param>
    /// <param name="recipientEmail">User's e-mail.</param>
    /// <param name="token">Token to reset password.</param>
    /// <param name="expirationDate">Expiration date of password reset token.</param>
    /// <returns>Message to send.</returns>
    IEmailMessage CreatePasswordResetEmailMessage(string recipientName,
                                                  string recipientEmail,
                                                  string token,
                                                  DateTime expirationDate);

    /// <summary>
    /// Creates an e-mail message with information about successfully reseted password.
    /// </summary>
    /// <param name="recipientName">User's name.</param>
    /// <param name="recipientEmail">User's e-mail.</param>
    /// <returns>Message to send.</returns>
    IEmailMessage CreatePasswordResetedEmailMessage(string recipientName,
                                                    string recipientEmail);
}