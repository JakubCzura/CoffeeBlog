using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity to store requests' details. It can be used in middleware that processes requests and responses.
/// </summary>
/// <param name="controllerName">Controller's name.</param>
/// <param name="path">Request's path.</param>
/// <param name="httpMethod">Http method like GET.</param>
/// <param name="statusCode">Response's status code.</param>
/// <param name="requestBody">Request's body.</param>
/// <param name="requestContentType">Request's content type.</param>
/// <param name="responseBody">Response's body.</param>
/// <param name="responseContentType">Response's content type.</param>
/// <param name="requestTimeInMiliseconds">Request's time specified in miliseconds.</param>
/// <param name="userId">Id of user who sends request.</param>
public class RequestDetail(string controllerName,
                           string path,
                           string httpMethod,
                           int statusCode,
                           string? requestBody,
                           string? requestContentType,
                           string? responseBody,
                           string? responseContentType,
                           long requestTimeInMiliseconds,
                           int? userId) : DbEntityBase
{
    /// <summary>
    /// Name of controller that handles request.
    /// </summary>
    public string ControllerName { get; set; } = controllerName;

    /// <summary>
    /// Path of request like /api/MyController/GetWeatherForecast.
    /// </summary>
    public string Path { get; set; } = path;

    /// <summary>
    /// HTTP method like GET, POST, PUT, PATCH, DELETE.
    /// </summary>
    public string HttpMethod { get; set; } = httpMethod;

    /// <summary>
    /// Status code of response for example 200/>.
    /// </summary>
    public int StatusCode { get; set; } = statusCode;

    /// <summary>
    /// Request's body. It can be null if request does not have a body.
    /// </summary>
    public string? RequestBody { get; set; } = requestBody;

    /// <summary>
    /// Request's content type. It can be null if request does not have a body.
    /// </summary>
    public string? RequestContentType { get; set; } = requestContentType;

    /// <summary>
    /// Response's body. It can be null if response does not have a body.
    /// </summary>
    public string? ResponseBody { get; set; } = responseBody;

    /// <summary>
    /// Response's content type. It can be null if response does not have a body.
    /// </summary>
    public string? ResponseContentType { get; set; } = responseContentType;

    /// <summary>
    /// Request's time from send request to get response. It's specified in miliseconds.
    /// </summary>
    public long RequestTimeInMiliseconds { get; set; } = requestTimeInMiliseconds;

    /// <summary>
    /// Date and time when request was sent.
    /// </summary>
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User's id. Request can be sent by a user who is not signed in yet, so the property is nullable.
    /// </summary>
    public int? UserId { get; set; } = userId;
}