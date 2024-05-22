using AuthService.Domain.Resources;
using FluentResults;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when username already exists, for example in database.
/// </summary>
public class UsernameExistsError() : Error(ErrorMessages.UsernameAlreadyExists)
{
}