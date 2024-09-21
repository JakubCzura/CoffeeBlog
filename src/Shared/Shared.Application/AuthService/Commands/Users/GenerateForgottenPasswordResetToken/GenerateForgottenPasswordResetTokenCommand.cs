using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Request command to generate password reset token for user who has forgotten password. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Email"> User's e-mail. </param>
public record GenerateForgottenPasswordResetTokenCommand(string Email) : IRequest<Result<ResponseBase>>;