using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class RequestDetailEntity(string controllerName,
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