namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Email message for user who has just created a new account.
/// </summary>
/// <param name="To">User's email.</param>
/// <param name="Subject">Email's subject about welcoming new user.</param>
/// <param name="Body">Email's body, for example with a nice message to welcome user.</param>
public record WelcomeEmailMessage(string To,
                                  string? Subject,
                                  string? Body) : IEmailMessage;