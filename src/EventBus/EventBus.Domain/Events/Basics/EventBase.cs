namespace EventBus.Domain.Events.Basics;

/// <summary>
/// Base class for events used to communicate between microservices.
/// </summary>
public abstract record EventBase
{
    protected EventBase(string eventPublisherName, 
                        string eventPublisherMicroserviceName)
    {
        EventPublisherName = eventPublisherName;
        EventPublisherMicroserviceName = eventPublisherMicroserviceName;
    }

    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the event was sent.
    /// </summary>
    public DateTime EventPublishedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Name of the event publisher.
    /// </summary>
    public string EventPublisherName { get; set; }

    /// <summary>
    /// Name of the microservice that contains publisher of the event.
    /// </summary>
    public string EventPublisherMicroserviceName { get; set; }
}