using NotificationProvider.Domain.Entities.Basics;
using NotificationProvider.Domain.Enums;

namespace NotificationProvider.Domain.Entities;

/// <summary>
/// Entity for email message.
/// </summary>
public class EmailMessage : DbEntityBase
{
    public string SenderName { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string RecipientEmail { get; set; } = string.Empty;

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public EmailMessageStatus MessageStatus { get; set; }

    public int? SmtpErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public int SendErrorCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime SentAt { get; set; }
}