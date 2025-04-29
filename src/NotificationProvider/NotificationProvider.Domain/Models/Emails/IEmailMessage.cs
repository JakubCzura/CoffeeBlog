namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Interface for email message. Helpful for factories for email's messages.
/// </summary>
public interface IEmailMessage
{
    /// <summary>
    /// Name of email's sender.
    /// </summary>
    string SenderName { get; init; }

    /// <summary>
    /// Email of email's sender.
    /// </summary>
    string SenderEmail { get; init; }

    /// <summary>
    /// Email of email's recipient.
    /// </summary>
    string RecipientEmail { get; init; }

    /// <summary>
    /// Email's subject.
    /// </summary>
    string? Subject { get; init; }

    /// <summary>
    /// Email's body, message that will be delivereted to recipient.
    /// </summary>
    string? Body { get; init; }
}