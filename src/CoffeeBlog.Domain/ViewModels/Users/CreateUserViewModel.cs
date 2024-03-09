namespace CoffeeBlog.Domain.ViewModels.Users;

public class CreateUserViewModel
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string JwtToken { get; set; } = string.Empty;
};