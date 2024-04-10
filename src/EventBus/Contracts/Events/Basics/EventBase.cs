namespace Contracts.Events.Basics;

/// <summary>
/// Base class for events.
/// </summary>
public class EventBase
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the event was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}