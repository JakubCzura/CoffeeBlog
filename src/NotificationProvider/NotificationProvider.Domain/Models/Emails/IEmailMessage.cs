namespace NotificationProvider.Domain.Models.Emails;

/// <summary>
/// Interface for email message. Helpful for factories for email's messages.
/// </summary>
public interface IEmailMessage
{
    /// <summary>
    /// Email's recipient.
    /// </summary>
    string To { get; init; }

    /// <summary>
    /// Email's subject.
    /// </summary>
    string? Subject { get; init; }

    /// <summary>
    /// Email's body, message that will be delivereted to recipient.
    /// </summary>
    string? Body { get; init; }
}