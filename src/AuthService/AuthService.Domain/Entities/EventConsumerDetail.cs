using AuthService.Domain.Entities.Basics;

namespace AuthService.Domain.Entities;

/// <summary>
/// Entity to store events' consumers details. Helpful for microservices architecture.
/// </summary>
public class EventConsumerDetail : DbEntityBase
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Date and time when the event was sent.
    /// </summary>
    public DateTime EventPublishedAt { get; set; }

    /// <summary>
    /// Name of the event.
    /// </summary>
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the event publisher.
    /// </summary>
    public string EventPublisherName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the microservice that contains publisher of the event.
    /// </summary>
    public string EventPublisherMicroserviceName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the event consumer.
    /// </summary>
    public string EventConsumerName { get; set; } = string.Empty;

    /// <summary>
    /// Message that is content of the event.
    /// </summary>
    public string EventMessage { get; set; } = string.Empty;
}