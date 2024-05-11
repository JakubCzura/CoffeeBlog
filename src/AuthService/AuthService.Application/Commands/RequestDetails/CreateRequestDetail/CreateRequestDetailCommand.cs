using MediatR;

namespace AuthService.Application.Commands.RequestDetails.CreateRequestDetail;

/// <summary>
/// Request command to save request details to database. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="ControllerName"> Name of controller that handled request. </param>
/// <param name="Path"> Path of request. </param>
/// <param name="HttpMethod"> HTTP method of request like GET or POST. </param>
/// <param name="StatusCode"> Status code of response like 200 or 404. </param>
/// <param name="RequestBody"> Request body of request. </param>
/// <param name="RequestContentType"> Request content type like application/json. </param>
/// <param name="ResponseBody"> Response body of request. </param>
/// <param name="ResponseContentType"> Response content type like application/json. </param>
/// <param name="RequestTimeInMiliseconds"> Request time measured in miliseconds. </param>
/// <param name="UserId"> Id of user who sent request. It can be null if user was not authorized but sent request, for example failed attempt to sign in. </param>
public record CreateRequestDetailCommand(string ControllerName,
                                         string Path,
                                         string HttpMethod,
                                         int StatusCode,
                                         string? RequestBody,
                                         string? RequestContentType,
                                         string? ResponseBody,
                                         string? ResponseContentType,
                                         long RequestTimeInMiliseconds,
                                         int? UserId) : IRequest<Unit>
{
    /// <summary>
    /// Date and time when request was sent.
    /// </summary>
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}