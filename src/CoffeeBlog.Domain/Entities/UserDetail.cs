using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity to store user details like last sign in, last password change etc.
/// </summary>
public class UserDetail : DbEntityBase
{
    /// <summary>
    /// Date and time when user created account.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the user's account was last updated. It can be for example password change.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Date and time when user last successfully signed in.
    /// </summary>
    public DateTime LastSuccessfullSignIn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when user last failed to sign in.
    /// </summary>
    public DateTime? LastFailedSignIn { get; set; }

    /// <summary>
    /// Date and time when user last changed username.
    /// </summary>
    public DateTime? LastUsernameChange { get; set; }

    /// <summary>
    /// Date and time when user last changed e-mail.
    /// </summary>
    public DateTime? LastEmailChange { get; set; }

    /// <summary>
    /// Date and time when user last changed password.
    /// </summary>
    public DateTime? LastPasswordChange { get; set; }

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; }
}