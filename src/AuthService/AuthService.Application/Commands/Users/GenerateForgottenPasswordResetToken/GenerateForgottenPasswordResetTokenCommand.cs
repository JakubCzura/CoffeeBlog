using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.GenerateForgottenPasswordResetToken;

/// <summary>
/// Request command to generate password reset token for user who has forgotten password. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Email"> User's e-mail. </param>
public record GenerateForgottenPasswordResetTokenCommand(string Email) : IRequest<Result<ViewModelBase>>;