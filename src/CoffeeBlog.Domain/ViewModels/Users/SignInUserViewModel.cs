namespace CoffeeBlog.Domain.ViewModels.Users;

/// <summary>
/// View model to return after user was signed in.
/// </summary>
public class SignInUserViewModel
{
    public string JwtToken { get; set; } = string.Empty;
}