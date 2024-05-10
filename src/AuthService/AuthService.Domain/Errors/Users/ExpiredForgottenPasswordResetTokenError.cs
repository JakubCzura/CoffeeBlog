using AuthService.Domain.Resources;
using FluentResults;

namespace AuthService.Domain.Errors.Users;

public class ExpiredForgottenPasswordResetTokenError() : Error(ErrorMessages.TokenHasExpired)
{
}