using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Domain.SettingsOptions.Database;

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

    /// <summary>
    /// Collection naame for <see cref="ApiError"/>
    /// </summary>
    public string ApiErrorCollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Collection name for <see cref="EmailMessageDetail"/>
    /// </summary>
    public string EmailMessageDetailCollectionName { get; set; } = string.Empty;

    /// <summary>
    /// Collection name for <see cref="EventConsumerDetail"/>
    /// </summary>
    public string EventConsumerDetailCollectionName { get; set; } = string.Empty;
}