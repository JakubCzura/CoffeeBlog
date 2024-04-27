using AuthService.Domain.Entities.Basics;

namespace AuthService.Domain.Entities;

/// <summary>
/// Entity for role that supports authorization process.
/// </summary>
public class Role : DbEntityBase
{
    /// <summary>
    /// Role's name like "Admin", "User" etc.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of the role. It can provide additional information.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the role was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Users who have this role.
    /// </summary>
    public List<User> Users { get; set; } = [];
}