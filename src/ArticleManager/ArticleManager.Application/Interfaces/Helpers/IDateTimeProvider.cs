namespace ArticleManager.Application.Interfaces.Helpers;

/// <summary>
/// Provider of date and time. Use it in services which need time provided.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Current date and time.
    /// </summary>
    public DateTime UtcNow { get; }

    /// <summary>
    /// The local date and time.
    /// </summary>
    public DateTime Now { get; }
}