namespace CoffeeBlog.Domain.SettingsOptions.Authentication;

public class AuthenticationOptions
{
    public const string AppsettingsKey = "Authentication";
    public AuthenticationJwtOptions Jwt { get; set; } = new();
}