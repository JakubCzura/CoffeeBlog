namespace CoffeeBlog.Application.Interfaces.Security.Password;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    public bool VerifyPassword(string password,
                               string passwordHash);
}