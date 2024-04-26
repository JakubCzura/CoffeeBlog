namespace NotificationProvider.Domain.SettingsOptions.Database;

public class DatabaseOptions
{
    /// <summary>
    /// Key for database settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "Database";
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string ApiErrorCollectionName { get; set; } = string.Empty;
    public string EmailMessageDetailCollectionName { get; set; } = string.Empty;
    public string EventConsumerDetailCollectionName { get; set; } = string.Empty;
}