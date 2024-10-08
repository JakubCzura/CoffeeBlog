﻿using FluentResults;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when a forgotten password reset token is invalid and user can't be authenticated.
/// </summary>
public class InvalidForgottenPasswordResetTokenError() : Error(ErrorMessages.TokenValueIsInvalid)
{
}