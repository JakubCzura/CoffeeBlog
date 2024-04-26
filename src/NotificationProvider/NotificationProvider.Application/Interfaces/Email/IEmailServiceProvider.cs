using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Interfaces.Email;

/// <summary>
/// Interface for operations related to e-mail for example sending e-mails.
/// </summary>
public interface IEmailServiceProvider
{
    /// <summary>
    /// Sends an e-mail using SMTP client.
    /// </summary>
    /// <param name="emailMessage">Message to send.</param>
    /// <returns>Response from server.</returns>
    Task<string> SendEmailAsync(IEmailMessage emailMessage,
                                CancellationToken cancellationToken);
}