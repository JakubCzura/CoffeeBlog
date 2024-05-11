namespace EventBus.Domain.Events.Basics;

/// <summary>
/// Base class for events used to communicate between microservices.
/// </summary>
/// <param name="EventPublisherName">Name of the event publisher.</param>
/// <param name="EventPublisherMicroserviceName">Name of the microservice that contains publisher of the event.</param>
public abstract record EventBase(string EventPublisherName,
                                 string EventPublisherMicroserviceName)
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the event was sent.
    /// </summary>
    public DateTime EventPublishedAt { get; set; } = DateTime.UtcNow;
}