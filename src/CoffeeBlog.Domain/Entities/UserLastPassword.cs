using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity to store the last passwords of user.
/// It is used to prevent the user from using the same credentials again.
/// For example when user changes password, new password can't be the same as the last passwords.
/// </summary>
public class UserLastPassword : DbEntityBase
{
    /// <summary>
    /// User's last password which can't be used again.
    /// </summary>
    public string LastPassword { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the last password was created. First time when user creates account, it is also the first password. Then when user changes password.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; }
}