using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Users.ResetForgottenPassword;

/// <summary>
/// Request command to reset forgotten password. It's handled using Mediatr and CQRS pattern.
/// </summary>
public record ResetForgottenPasswordCommand(string Email,
                                            string NewPassword,
                                            string ConfirmNewPassword,
                                            string ForgottenPasswordResetToken) : IRequest<Result<ViewModelBase>>;