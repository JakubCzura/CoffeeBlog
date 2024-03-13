using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity for API error for example when an exception is thrown.
/// </summary>
/// <param name="exception">Exception thrown by application.</param>
/// <param name="message">Message of thrown exception.</param>
/// <param name="description">Exception's description defined in code.</param>
/// <param name="createdAt">Date and time when the error was thrown.</param>
public class ApiError(string exception,
                      string message,
                      string description,
                      DateTime createdAt) : DbEntityBase
{
    /// <summary>
    /// Exception thrown by application. It should be achieved by calling <c>exception.ToString()</c>.
    /// </summary>
    public string Exception { get; set; } = exception;

    /// <summary>
    /// Message of thrown exception. It should be achieved by calling <c>exception.Message</c>.
    /// </summary>
    public string Message { get; set; } = message;

    /// <summary>
    /// Additional information about error. For example there can be given information about situation or class where the exception was thrown.
    /// </summary>
    public string Description { get; set; } = description;

    /// <summary>
    /// Date and time when the error was thrown.
    /// </summary>
    public DateTime CreatedAt { get; set; } = createdAt;
}