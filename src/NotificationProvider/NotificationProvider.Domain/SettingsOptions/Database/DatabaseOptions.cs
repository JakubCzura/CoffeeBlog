namespace NotificationProvider.Domain.SettingsOptions.Database;

/// <summary>
/// Configuration options for database in appsettings.json.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Key for database settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "Database";

    /// <summary>
    /// Connection string to the database.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Name of the database.
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;
}