using MediatR;

namespace CoffeeBlog.Domain.Commands.ApiErrors;

/// <summary>
/// Request command to create API error. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class CreateApiErrorCommand : IRequest<Unit>
{
    /// <summary>
    /// Exception name. It should be achieved by calling <c>exception.GetType().FullName</c>.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Exception thrown by application. It should be achieved by calling <c>exception.ToString()</c>.
    /// </summary>
    public string Exception { get; set; } = string.Empty;

    /// <summary>
    /// Message of thrown exception. It should be achieved by calling <c>exception.Message</c>.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional information about error, for example there can be given information about situation or class where the exception was thrown.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}