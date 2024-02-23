using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class UserToRoleEntity : DbEntityBase
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}