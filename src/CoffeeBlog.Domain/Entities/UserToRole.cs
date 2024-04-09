using AuthService.Domain.Entities.DbEntitiesBase;

namespace AuthService.Domain.Entities;

/// <summary>
/// Entity to define relation between <see cref="User"/> and <see cref="Role"/>.
/// </summary>
public class UserToRole : DbEntityBase
{
    /// <summary>
    /// User's id.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Role's id.
    /// </summary>
    public int RoleId { get; set; }
}