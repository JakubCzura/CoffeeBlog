using CoffeeBlog.Domain.Entities.Base;

namespace CoffeeBlog.Domain.Entities.Users;

public class UserToRole : DbEntityBase
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}