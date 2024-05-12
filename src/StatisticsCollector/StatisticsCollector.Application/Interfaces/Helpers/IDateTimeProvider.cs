namespace StatisticsCollector.Application.Interfaces.Helpers;

/// <summary>
/// Interface to deliver abstracted time and date properties. It is helpful for testing purposes. Use it in repositories and services which need time provided.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// The current time in UTC format.
    /// </summary>
    public DateTime UtcNow { get; }

    /// <summary>
    /// The current time in local format.
    /// </summary>
    public DateTime Now { get; }

    /// <summary>
    /// Converts <see cref="DateTime"/> to <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="dateTime">Date and time to convert</param>
    /// <returns><see cref="DateOnly"/> instance</returns>
    public DateOnly FromDateTime(DateTime dateTime);
}