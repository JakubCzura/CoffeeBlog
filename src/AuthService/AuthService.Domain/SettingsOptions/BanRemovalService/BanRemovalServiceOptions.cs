namespace AuthService.Domain.SettingsOptions.BanRemovalService;

/// <summary>
/// Configuration options for BanRemovalService in appsettings.json.
/// </summary>
public class BanRemovalServiceOptions
{
    /// <summary>
    /// Key for database settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "BanRemovalService";

    /// <summary>
    /// Hour when the collector should start working.
    /// </summary>
    public int StartHour { get; set; }

    /// <summary>
    /// Minute when the collector should start working.
    /// </summary>
    public int StartMinute { get; set; }

    /// <summary>
    /// Second when the collector should start working.
    /// </summary>
    public int StartSecond { get; set; }

    /// <summary>
    /// Interval in hours between each run of the collector.
    /// </summary>
    public int IntervalInHours { get; set; }
}