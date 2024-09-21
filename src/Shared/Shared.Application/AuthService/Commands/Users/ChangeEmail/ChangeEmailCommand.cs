using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Users.ChangeEmail;

/// <summary>
/// Request command to change user's e-mail. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="NewEmail"> User's new e-mail. </param>
public record ChangeEmailCommand(string NewEmail) : IRequest<Result<ResponseBase>>;