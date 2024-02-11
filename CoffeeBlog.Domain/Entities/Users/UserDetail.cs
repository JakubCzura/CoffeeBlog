namespace CoffeeBlog.Domain.Entities.Users;

public class UserDetail
{
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime LastSuccessfullSignIn { get; set; } = DateTime.UtcNow;

    public DateTime? LastFailedSignIn { get; set; }

    public DateTime? LastUsernameChange { get; set; }

    public DateTime? LastEmailChange { get; set; }

    public DateTime? LastPasswordChange { get; set; }
}