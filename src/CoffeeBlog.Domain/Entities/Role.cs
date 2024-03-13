using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

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
    /// Users who have this role.
    /// </summary>
    public List<User> Users { get; set; } = [];
}