﻿using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.AuthService.Commands.Users.ChangePassword;

/// <summary>
/// Request command to change user's password. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="CurrentPassword"> User's current password to ensure that user is authorized to change password. </param>
/// <param name="NewPassword"> User's new password. It's going to be hashed when saving to database. </param>
/// <param name="ConfirmNewPassword"> It must match <see cref="NewPassword"/>. </param>
public record ChangePasswordCommand(string CurrentPassword,
                                    string NewPassword,
                                    string ConfirmNewPassword) : IRequest<Result<ResponseBase>>;