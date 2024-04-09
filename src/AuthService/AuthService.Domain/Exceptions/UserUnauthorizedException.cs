using AuthService.Domain.Resources;

namespace AuthService.Domain.Exceptions;

/// <summary>
/// Exception thrown when user is not authorized, for example when user is not signed in and tries to change e-mail.
/// </summary>
public class UserUnauthorizedException() : Exception(ExceptionMessages.UserUnauthorized)
{
}