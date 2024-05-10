using AuthService.Domain.Resources;
using FluentResults;

namespace AuthService.Domain.Errors.Users;

public class InvalidForgottenPasswordResetTokenError() : Error(ErrorMessages.TokenValueIsInvalid)
{
}