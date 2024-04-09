using AuthService.Domain.Entities.DbEntitiesBase;

namespace AuthService.Domain.Entities;

/// <summary>
/// Entity to store user details like last sign in, last password change etc.
/// This entity should be created only once when user creates an account, then it should be updated.
/// When user creates an account this fact should be noticed as <see cref="LastSuccessfullSignIn"/> .
/// </summary>
public class UserDetail(int userId) : DbEntityBase
{

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
    public int UserId { get; set; } = userId;
}