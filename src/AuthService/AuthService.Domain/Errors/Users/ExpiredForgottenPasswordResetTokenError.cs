using FluentResults;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when a forgotten password reset token has expired.
/// </summary>
public class ExpiredForgottenPasswordResetTokenError() : Error(ErrorMessages.TokenHasExpired)
{
}