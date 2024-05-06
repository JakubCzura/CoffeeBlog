using MongoDB.Bson.Serialization.Attributes;
using NotificationProvider.Domain.Entities.Basics;

namespace NotificationProvider.Domain.Entities;

/// <summary>
/// Entity for API error for example when an exception is thrown.
/// </summary>
public class ApiError : DbEntityBase
{
    /// <summary>
    /// Name of exception.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Exception thrown by application.
    /// </summary>
    public string Exception { get; set; } = string.Empty;

    /// <summary>
    /// Message of thrown exception.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional information about error.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the error was thrown.
    /// </summary>
    public DateTime ThrownAt { get; set; } = DateTime.UtcNow;
}