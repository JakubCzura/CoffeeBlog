using FluentResults;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when a forgotten password reset token has expired.
/// </summary>
public class ExpiredForgottenPasswordResetTokenError() : Error(ErrorMessages.TokenHasExpired)
{
}