using AuthService.Domain.Models.Emails;

namespace AuthService.Application.Email;

/// <summary>
/// Interface for operations related to e-mail for example sending e-mails.
/// </summary>
public interface IEmailServiceProvider
{
    /// <summary>
    /// Sends an e-mail using STMP client.
    /// </summary>
    /// <param name="emailMessage">Message to send.</param>
    /// <returns>Response from server.</returns>
    Task<string> SendEmailAsync(IEmailMessage emailMessage);
}