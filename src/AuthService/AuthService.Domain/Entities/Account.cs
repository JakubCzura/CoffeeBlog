using AuthService.Domain.Entities.Basics;
using Shared.Domain.AuthService.Enums;

namespace AuthService.Domain.Entities;

/// <summary>
/// This entity represents user account.
/// </summary>
public class Account(int userId) : DbEntityBase
{
    /// <summary>
    /// If true, user can't sign in.
    /// </summary>
    public bool IsBanned { get; set; } = false;

    /// <summary>
    /// Account ban reason.
    /// If account is not banned this field should be null.
    /// </summary>
    public AccountBanReason? BanReason { get; set; }

    /// <summary>
    /// Ban note. It's used to describe why account is banned.
    /// If account is not banned this field should be null.
    /// </summary>
    public string? BanNote { get; set; }

    /// <summary>
    /// Date when account was banned.
    /// If account is not banned this field should be null
    /// </summary>
    public DateOnly? BannedAt { get; set; }

    /// <summary>
    /// Date when account ban ends.
    /// If account is banned forever this field should have value <see cref="DateOnly.MaxValue"/>.
    /// If account is not banned this field should be null.
    /// For example if this property has value 2022-01-01 and the date is 2022-01-02, ban will be removed.
    /// We expect that this field's value is also the day when account is banned, when the day ends account should be unbanned.
    /// </summary>
    public DateOnly? BanEndsAt { get; set; }

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; } = userId;
}