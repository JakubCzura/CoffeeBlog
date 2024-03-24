using MediatR;

namespace CoffeeBlog.Domain.Commands.RequestDetails;

/// <summary>
/// Request command to save request details to database. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class CreateRequestDetailCommand : IRequest<Unit>
{
    /// <summary>
    /// Name of controller that handled request.
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    /// Path of request.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// HTTP method of request like GET or POST.
    /// </summary>
    public string HttpMethod { get; set; } = string.Empty;

    /// <summary>
    /// Status code of response like 200 or 404.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Request body of request.
    /// </summary>
    public string? RequestBody { get; set; }

    /// <summary>
    /// Request content type like application/json.
    /// </summary>
    public string? RequestContentType { get; set; }

    /// <summary>
    /// Response body of request.
    /// </summary>
    public string? ResponseBody { get; set; }

    /// <summary>
    /// Response content type like application/json.
    /// </summary>
    public string? ResponseContentType { get; set; }

    /// <summary>
    /// Request time measured in miliseconds.
    /// </summary>
    public long RequestTimeInMiliseconds { get; set; }

    /// <summary>
    /// Id of user who sent request. It can be null if user was not authorized but sent request, for example failed attempt to sign in.
    /// </summary>
    public int? UserId { get; set; }
}