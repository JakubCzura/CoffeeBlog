namespace NotificationProvider.Domain.SettingsOptions.Email;

/// <summary>
/// Configuration options for e-mails sent by CoffeeBlog application based on appsettings.json.
/// </summary>
public class EmailCoffeeBlogOptions
{
    /// <summary>
    /// Name of application, for example CoffeeBlog.
    /// </summary>
    public string SenderName { get; set; } = string.Empty;

    /// <summary>
    /// Email associated with CoffeeBlog application.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Password for email associated with CoffeeBlog application.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}