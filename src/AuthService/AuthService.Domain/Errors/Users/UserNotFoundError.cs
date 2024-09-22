using FluentResults;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when user is not found, for example in database.
/// </summary>
public class UserNotFoundError() : Error(ErrorMessages.UserNotFound)
{
}