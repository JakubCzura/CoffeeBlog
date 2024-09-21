namespace Shared.Domain.AuthService.Enums;

/// <summary>
/// Enum representing reason of account ban.
/// </summary>
public enum AccountBanReason
{
    /// <summary>
    /// User has tried to sign in too many times with wrong credentials.
    /// </summary>
    TooManyFailedSignInAttempts,

    /// <summary>
    /// User behaves offensively.
    /// /// </summary>
    OffensiveBehaviour,

    /// <summary>
    /// User writes spam and nonsense posts.
    /// </summary>
    Spamming,

    /// <summary>
    /// The reason of ban is other than specified in enum.
    /// </summary>
    Other
}