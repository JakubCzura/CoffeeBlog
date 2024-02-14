using CoffeeBlog.Domain.Entities.Base;
using CoffeeBlog.Domain.Entities.Users;

namespace CoffeeBlog.Domain.Entities.Roles;

public class Role : DbEntityBase
{
    public string Name { get; set; } = string.Empty;
    public List<User> Users { get; set; } = [];
}