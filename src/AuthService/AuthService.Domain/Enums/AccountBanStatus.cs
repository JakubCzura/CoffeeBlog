namespace AuthService.Domain.Enums;

/// <summary>
/// Enum representing reason of account ban.
/// </summary>
public enum AccountBanReason
{
    /// <summary>
    /// The reason of ban is unspecified. This should be default value for sql column.
    /// </summary>
    Unspecified,

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
    Spamming
}