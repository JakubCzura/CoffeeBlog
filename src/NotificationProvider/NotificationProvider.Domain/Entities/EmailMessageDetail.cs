using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

/// <summary>
/// Entity for email message detail.
/// </summary>
public class EmailMessageDetail : DbEntityBase
{
    public string SenderName { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string RecipientName { get; set; } = string.Empty;

    public string RecipientEmail { get; set; } = string.Empty;

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}