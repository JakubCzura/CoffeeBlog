using AuthService.Domain.Entities.Basics;
using AuthService.Domain.Enums;

namespace AuthService.Domain.Entities;

/// <summary>
/// This entity represents user account.
/// </summary>
public class UserAccount(int userId) : DbEntityBase
{
    /// <summary>
    /// If true, user can't sign in.
    /// </summary>
    public bool IsBanned { get; set; } = false;

    /// <summary>
    /// Account ban reason. If account is not banned this field should have value <see cref="AccountBanReason.Unspecified"/>.
    /// Prefer default value instead of null as this field doesn't matter if <see cref="IsBanned"/> is false.
    /// </summary>
    public AccountBanReason AccountBanReason { get; set; } = AccountBanReason.Unspecified;

    /// <summary>
    /// Ban note. It's used to describe why account is banned. If account is not banned this field should be empty.
    /// Prefer default value instead of null as this field doesn't matter if <see cref="IsBanned"/> is false.
    /// </summary>
    public string BanNote { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when account was banned.
    /// </summary>
    public DateTime BannedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when account ban ends. If account is banned forever this field should have value <see cref="DateTime.MaxValue"/>.
    /// </summary>
    public DateTime BanEndsAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; } = userId;
}