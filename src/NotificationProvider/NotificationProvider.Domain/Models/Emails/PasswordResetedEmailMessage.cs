namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Email message for user who has just reseted password.
/// </summary>
/// <param name="SenderName">Sender's name.</param>
/// <param name="SenderEmail">Sender's e-mail.</param>
/// <param name="RecipientEmail">User's name.</param>
/// <param name="RecipientName">User's name.</param>
/// <param name="Subject">Email's subject about successfully reseted password.</param>
/// <param name="Body">Email's body with information that user has reseted password.</param>
public record PasswordResetedEmailMessage(string SenderName,
                                          string SenderEmail,
                                          string RecipientName,
                                          string RecipientEmail,
                                          string? Subject,
                                          string? Body) : IEmailMessage;