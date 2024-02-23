using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class UserDetailEntity : DbEntityBase
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastSuccessfullSignIn { get; set; } = DateTime.UtcNow;
    public DateTime? LastFailedSignIn { get; set; }
    public DateTime? LastUsernameChange { get; set; }
    public DateTime? LastEmailChange { get; set; }
    public DateTime? LastPasswordChange { get; set; }

    public int UserId { get; set; }
}