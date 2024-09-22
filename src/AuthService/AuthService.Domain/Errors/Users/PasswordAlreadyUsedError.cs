using FluentResults;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when password has already been used, for example when user wants to change password
/// and tries to use the same password again.
/// </summary>
public class PasswordAlreadyUsedError() : Error(ErrorMessages.YouHaveAlreadyUsedThisPassword)
{
}