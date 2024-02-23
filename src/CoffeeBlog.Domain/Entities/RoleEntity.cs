using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class RoleEntity : DbEntityBase
{
    public string Name { get; set; } = string.Empty;

    public List<UserEntity> Users { get; set; } = [];
}