namespace CoffeeBlog.Domain.SettingsOptions.UserCredential;

/// <summary>
/// Configuration options for user credential.
/// </summary>
public class UserCredentialOptions
{
    /// <summary>
    /// Key for user credential settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "UserCredential";

    /// <summary>
    /// When user wants to change password, the new password must be different from the last passwords.
    /// This variable is the count of last passwords that will be checked.
    /// </summary>
    public int LastPasswordCount { get; set; }
}