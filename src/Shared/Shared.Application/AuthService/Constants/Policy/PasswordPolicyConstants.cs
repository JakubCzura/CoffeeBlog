namespace Shared.Application.AuthService.Constants.Policy;

/// <summary>
/// Constants for password policy.
/// </summary>
public class PasswordPolicyConstants
{
    /// <summary>
    /// Special characters allowed in password.
    /// </summary>
    public const string SpecialCharacters = "!@#$%^&*()_-+=:<>.?";

    /// <summary>
    /// Password's min length.
    /// </summary>
    public const int MinLength = 5;

    /// <summary>
    /// Password's max length.
    /// </summary>
    public const int MaxLength = 50;
}