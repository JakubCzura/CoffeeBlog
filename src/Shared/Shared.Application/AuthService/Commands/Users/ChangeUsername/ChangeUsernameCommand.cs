using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Users.ChangeUsername;

/// <summary>
/// Request command to change user's username. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="NewUsername"> User's new username. </param>
public record ChangeUsernameCommand(string NewUsername) : IRequest<Result<ResponseBase>>;