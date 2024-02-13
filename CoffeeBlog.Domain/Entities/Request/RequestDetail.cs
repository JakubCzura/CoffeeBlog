using CoffeeBlog.Domain.Entities.EntityBase;

namespace CoffeeBlog.Domain.Entities.Request;

public class RequestDetail(string controllerName,
                           string path,
                           string httpMethod,
                           int statusCode,
                           string? requestBody,
                           string? requestContentType,
                           string? responseBody,
                           string? responseContentType,
                           long requestTimeInMiliseconds,
                           DateTime sentAt) : DbEntityBase
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
}