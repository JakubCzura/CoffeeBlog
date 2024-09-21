using FluentResults;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when user is not found, for example in database.
/// </summary>
public class UserNotFoundError() : Error(ErrorMessages.UserNotFound)
{
}