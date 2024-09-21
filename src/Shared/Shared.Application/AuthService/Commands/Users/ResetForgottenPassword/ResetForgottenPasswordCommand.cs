using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Users.ResetForgottenPassword;

/// <summary>
/// Request command to reset forgotten password. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Email"> User's e-mail. </param>
/// <param name="NewPassword"> User's new password that will be hashed in database. </param> -->
/// <param name="ConfirmNewPassword"> It must match <see cref="NewPassword"/>. </param>
/// <param name="ForgottenPasswordResetToken"> Token that was sent to user's e-mail. User must type this token due to authorization purposes.</param>
public record ResetForgottenPasswordCommand(string Email,
                                            string NewPassword,
                                            string ConfirmNewPassword,
                                            string ForgottenPasswordResetToken) : IRequest<Result<ResponseBase>>;