using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity to store requests' details. It can be used in middleware that processes requests and responses.
/// </summary>
public class RequestDetail : DbEntityBase
{
    /// <summary>
    /// Name of controller that handles request.
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    /// Path of request like /api/MyController/GetWeatherForecast.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// HTTP method like GET, POST, PUT, PATCH, DELETE.
    /// </summary>
    public string HttpMethod { get; set; } = string.Empty;

    /// <summary>
    /// Status code of response for example 200/>.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Request's body. It can be null if request does not have a body.
    /// </summary>
    public string? RequestBody { get; set; }

    /// <summary>
    /// Request's content type. It can be null if request does not have a body.
    /// </summary>
    public string? RequestContentType { get; set; }

    /// <summary>
    /// Response's body. It can be null if response does not have a body.
    /// </summary>
    public string? ResponseBody { get; set; }

    /// <summary>
    /// Response's content type. It can be null if response does not have a body.
    /// </summary>
    public string? ResponseContentType { get; set; }

    /// <summary>
    /// Request's time from send request to get response. It's specified in miliseconds.
    /// </summary>
    public long RequestTimeInMiliseconds { get; set; }

    /// <summary>
    /// Date and time when request was sent.
    /// </summary>
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User's id. Request can be sent by a user who is not signed in yet, so the property is nullable.
    /// </summary>
    public int? UserId { get; set; }
}