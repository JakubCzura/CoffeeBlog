namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Email message for user who wants to reset password.
/// </summary>
/// <param name="SenderName">Sender's name.</param>
/// <param name="SenderEmail">Sender's e-mail.</param>
/// <param name="RecipientEmail">User's name.</param>
/// <param name="RecipientEmail">User's e-mail.</param>
/// <param name="Subject">Email's subject about resetting password.</param>
/// <param name="Body">Email's body with information how to reset password, for example it can contain token to reset password.</param>
public record PasswordResetEmailMessage(string SenderName,
                                        string SenderEmail,
                                        string RecipientName,
                                        string RecipientEmail,
                                        string? Subject,
                                        string? Body) : IEmailMessage;