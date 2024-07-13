namespace StatisticsCollector.Application.Interfaces.Helpers;

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

    /// <summary>
    /// Converts <see cref="DateTime"/> to <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="dateTime">Date and time to convert</param>
    /// <returns><see cref="DateOnly"/> instance</returns>
    public DateOnly FromDateTime(DateTime dateTime);
}