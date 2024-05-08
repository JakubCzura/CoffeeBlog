using AuthService.Domain.Entities.Basics;
using AuthService.Domain.Enums;

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
    /// Date and time when account was banned. 
    /// If account is not banned this field should be null
    /// </summary>
    public DateTime? BannedAt { get; set; }

    /// <summary>
    /// Date and time when account ban ends. 
    /// If account is banned forever this field should have value <see cref="DateTime.MaxValue"/>.
    /// If account is not banned this field should be null
    /// </summary>
    public DateTime? BanEndsAt { get; set; }

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; } = userId;
}