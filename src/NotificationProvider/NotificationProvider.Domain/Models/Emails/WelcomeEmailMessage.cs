namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Email message for user who has just created a new account.
/// </summary>
/// <param name="SenderName">Sender's name.</param>
/// <param name="SenderEmail">Sender's e-mail.</param>
/// <param name="RecipientName">User's name.</param>
/// <param name="RecipientEmail">User's e-mail.</param>
/// <param name="Subject">Email's subject about resetting password.</param>
/// <param name="Body">Email's body, for example with a nice message to welcome user.</param>
public record WelcomeEmailMessage(string SenderName,
                                  string SenderEmail,
                                  string RecipientName,
                                  string RecipientEmail,
                                  string? Subject,
                                  string? Body) : IEmailMessage;