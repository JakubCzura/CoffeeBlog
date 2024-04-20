namespace EventBus.Domain.Events.Basics;

/// <summary>
/// Base class for events used to communicate between microservices.
/// </summary>
public abstract record EventBase
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the event was created.
    /// </summary>
    public DateTime EventCreatedAt { get; set; } = DateTime.UtcNow;
}