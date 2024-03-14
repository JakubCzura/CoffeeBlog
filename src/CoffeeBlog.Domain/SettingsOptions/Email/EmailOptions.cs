namespace CoffeeBlog.Domain.SettingsOptions.Email;

/// <summary>
/// Configuration options for e-mail provder based on appsettings.json.
/// </summary>
public class EmailOptions
{
    /// <summary>
    /// Key for e-mail settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "Email";

    /// <summary>
    /// Configuration options for SMTP server.
    /// </summary>
    public EmailSmtpOptions Smtp { get; set; } = new();

    /// <summary>
    /// Configuration options for e-mail sent by CoffeeBlog application.
    /// </summary>
    public EmailCoffeeBlogOptions CoffeeBlog { get; set; } = new();
}