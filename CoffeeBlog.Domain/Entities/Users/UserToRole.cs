using CoffeeBlog.Domain.Entities.EntityBase;

namespace CoffeeBlog.Domain.Entities.Users;

public class UserToRole : DbEntityBase
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}