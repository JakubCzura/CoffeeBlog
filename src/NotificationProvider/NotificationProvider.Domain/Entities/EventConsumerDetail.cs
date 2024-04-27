using MongoDB.Bson.Serialization.Attributes;
using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

public class EventConsumerDetail : DbEntityBase
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    [BsonElement("EventId")]
    public Guid EventId { get; set; }

    /// <summary>
    /// Date and time when the event was sent.
    /// </summary>
    [BsonElement("EventPublishedAt")]
    public DateTime EventPublishedAt { get; set; }

    /// <summary>
    /// Name of the event.
    /// </summary>
    [BsonElement("EventName")]
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the event publisher.
    /// </summary>
    [BsonElement("EventPublisherName")]
    public string EventPublisherName { get; set; } = string.Empty;

    /// <summary>
    /// Name of the event consumer.
    /// </summary>
    [BsonElement("EventConsumerName")]
    public string EventConsumerName { get; set; } = string.Empty;

    /// <summary>
    /// Message that is content of the event.
    /// </summary>
    [BsonElement("EventMessage")]
    public string EventMessage { get; set; } = string.Empty;
}