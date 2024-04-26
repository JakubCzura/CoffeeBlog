using MongoDB.Bson.Serialization.Attributes;
using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

/// <summary>
/// Entity for API error for example when an exception is thrown.
/// </summary>
public class ApiError : MongoDbEntityBase
{
    /// <summary>
    /// Name of exception.
    /// </summary>
    [BsonElement("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Exception thrown by application.
    /// </summary>
    [BsonElement("Exception")]
    public string Exception { get; set; } = string.Empty;

    /// <summary>
    /// Message of thrown exception.
    /// </summary>
    [BsonElement("Message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional information about error.
    /// </summary>
    [BsonElement("Description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the error was thrown.
    /// </summary>
    [BsonElement("ThrownAt")]
    public DateTime ThrownAt { get; set; } = DateTime.UtcNow;
}