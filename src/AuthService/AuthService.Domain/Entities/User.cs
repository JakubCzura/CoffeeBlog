using AuthService.Domain.Entities.Basics;

namespace AuthService.Domain.Entities;

/// <summary>
/// Entity that represents user of the application who has created an account.
/// </summary>
public class User : DbEntityBase
{
    /// <summary>
    /// User's unique username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User's unique e-mail.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password. It's always hashed and must not be decrypted.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when token to reset password expires.
    /// It is null by default and must be updated every time user asks to reset forgotten password.
    /// It is combined with <see cref="ForgottenPasswordResetToken"/> to verify token validation.
    /// </summary>
    public DateTime? ForgottenPasswordResetTokenExpiresAt { get; set; }

    /// <summary>
    /// Token to reset user's forgotten password.
    /// It's used to verify user's identity when user asks to reset forgotten password.
    /// It's null by default and must be updated every time user asks to reset forgotten password.
    /// It is combined with <see cref="ForgottenPasswordResetTokenExpiresAt"/> to verify token validation.
    /// </summary>
    public string? ForgottenPasswordResetToken { get; set; }

    /// <summary>
    /// List of user's last credentials. It's used to prevent user from using the same password when changing it.
    /// </summary>
    public virtual List<UserLastPassword> LastPasswords { get; set; } = [];

    /// <summary>
    /// Roles assigned to the user. It's used to define user's permissions.
    /// </summary>
    public virtual List<Role> Roles { get; set; } = [];

    /// <summary>
    /// Requests sent by the user.
    /// </summary>
    public virtual List<RequestDetail> RequestDetails { get; set; } = [];
}