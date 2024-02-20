using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class Role : DbEntityBase
{
    public string Name { get; set; } = string.Empty;

    public List<User> Users { get; set; } = [];
}