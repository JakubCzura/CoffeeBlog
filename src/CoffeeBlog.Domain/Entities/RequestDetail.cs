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
/// <param name="sentAt">Date and time when request was sent.</param>
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
                           DateTime sentAt,
                           int? userId) : DbEntityBase
{
    public string ControllerName { get; set; } = controllerName;
    public string Path { get; set; } = path;
    public string HttpMethod { get; set; } = httpMethod;
    public int StatusCode { get; set; } = statusCode;
    public string? RequestBody { get; set; } = requestBody;
    public string? RequestContentType { get; set; } = requestContentType;
    public string? ResponseBody { get; set; } = responseBody;
    public string? ResponseContentType { get; set; } = responseContentType;
    public long RequestTimeInMiliseconds { get; set; } = requestTimeInMiliseconds;
    public DateTime SentAt { get; set; } = sentAt;

    /// <summary>
    /// Request can be sent by a user who is not logged in yet, so the property is nullable.
    /// </summary>
    public int? UserId { get; set; } = userId;
}