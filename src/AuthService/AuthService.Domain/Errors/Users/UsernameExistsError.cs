using FluentResults;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when username already exists, for example in database.
/// </summary>
public class UsernameExistsError() : Error(ErrorMessages.UsernameAlreadyExists)
{
}