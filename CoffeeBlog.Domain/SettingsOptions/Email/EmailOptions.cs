namespace CoffeeBlog.Domain.SettingsOptions.Email;

public class EmailOptions
{
    public const string AppsettingsKey = "Email";
    public EmailSmtpOptions Smtp { get; set; } = new();
    public EmailCoffeeBlogOptions CoffeeBlog { get; set; } = new();
}