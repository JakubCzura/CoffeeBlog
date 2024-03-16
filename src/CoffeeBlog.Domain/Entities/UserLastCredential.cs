using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Entity to store the last credentials of user.
/// It is used to prevent the user from using the same credentials again.
/// For example when user changes password, new password can't be the same as the last passwords.
/// </summary>
public class UserLastCredential : DbEntityBase
{
    /// <summary>
    /// First of three last passwords used by user. Passwords are always hashed.
    /// </summary>
    public string? LastPassword1 { get; set; }

    /// <summary>
    /// Second of three last passwords used by user. Passwords are always hashed.
    /// </summary>
    public string? LastPassword2 { get; set; }

    /// <summary>
    /// Third of three last passwords used by user. Passwords are always hashed.
    /// </summary>
    public string? LastPassword3 { get; set; }

    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; }
}