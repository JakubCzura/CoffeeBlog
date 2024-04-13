namespace EventBus.Domain.SettingsOptions.EventBus;

/// <summary>
/// Configuration options for event bus based on appsettings.json.
/// </summary>
public class EventBusOptions
{
    /// <summary>
    /// Key for event bus settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "EventBus";

    /// <summary>
    /// Configuration options for host address.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// Configuration options for username for connection.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Configuration options for password for connection.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}