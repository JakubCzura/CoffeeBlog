using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotificationProvider.Domain.Entities.Basics;

public abstract class MongoDbEntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
}