using MediatR;

namespace Shared.Application.AuthService.Commands.ApiErrors.CreateApiError;

/// <summary>
/// Request command to save API error to database. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Name"> Exception name. It should be achieved by calling <c>exception.GetType().Name</c>. </param>
/// <param name="Exception"> Exception thrown by application. It should be achieved by calling <c>exception.ToString()</c>. </param>
/// <param name="Message"> Message of thrown exception. It should be achieved by calling <c>exception.Message</c>. </param>
/// <param name="Description"> Additional information about error, for example there can be given information about situation or class where the exception was thrown. </param>
public record CreateApiErrorCommand(string Name,
                                    string Exception,
                                    string Message,
                                    string Description) : IRequest<Unit>;