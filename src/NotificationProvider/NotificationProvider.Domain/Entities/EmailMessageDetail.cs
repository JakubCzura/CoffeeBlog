using MongoDB.Bson.Serialization.Attributes;
using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

public class EmailMessageDetail : MongoDbEntityBase
{
    [BsonElement("SenderName")]
    public string SenderName { get; set; } = string.Empty;

    [BsonElement("SenderEmail")]
    public string SenderEmail { get; set; } = string.Empty;

    [BsonElement("RecipientName")]
    public string RecipientName { get; set; } = string.Empty;

    [BsonElement("RecipientEmail")]
    public string RecipientEmail { get; set; } = string.Empty;

    [BsonElement("Subject")]
    public string? Subject { get; set; }

    [BsonElement("Body")]
    public string? Body { get; set; }

    [BsonElement("SentAt")]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}