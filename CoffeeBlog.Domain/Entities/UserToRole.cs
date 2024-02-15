using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class UserToRole : DbEntityBase
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}