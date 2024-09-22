using FluentResults;
using Shared.Domain.Common.Resources.Translations;

namespace AuthService.Domain.Errors.Users;

/// <summary>
/// Error that occurs when email already exists, for example in database.
/// </summary>
public class EmailExistsError() : Error(ErrorMessages.EmailAlreadyExists);