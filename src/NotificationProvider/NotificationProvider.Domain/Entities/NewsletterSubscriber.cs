using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

/// <summary>
/// Entity for newsletter subscribers.
/// </summary>
public class NewsletterSubscriber : DbEntityBase
{
    /// <summary>
    /// Email of the subscriber to get notifications.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Indicates if the subscriber agrees to the terms and conditions.
    /// </summary>
    public bool AgreeToTerms { get; set; }

    /// <summary>
    /// Indicates if the subscription is confirmed.
    /// </summary>
    public bool IsConfirmed { get; set; }
}