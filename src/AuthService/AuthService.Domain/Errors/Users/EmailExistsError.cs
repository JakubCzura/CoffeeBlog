using FluentResults;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when email already exists, for example in database.
/// </summary>
public class EmailExistsError() : Error(ErrorMessages.EmailAlreadyExists);