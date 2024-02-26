using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class User : DbEntityBase
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual List<UserLastCredential> LastCredentials { get; set; } = [];
    public virtual List<Role> Roles { get; set; } = [];
    public virtual List<RequestDetail> RequestDetails { get; set; } = [];
}