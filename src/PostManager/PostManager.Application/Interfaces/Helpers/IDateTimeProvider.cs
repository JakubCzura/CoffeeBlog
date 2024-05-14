namespace PostManager.Application.Interfaces.Helpers;

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
}