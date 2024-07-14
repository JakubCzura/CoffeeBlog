namespace StatisticsCollector.Domain.SettingsOptions.UsersDiagnosticsCollector;

/// <summary>
/// Configuration options for UsersDiagnosticsCollector in appsettings.json.
/// </summary>
public class UsersDiagnosticsCollectorOptions
{
    /// <summary>
    /// Key for database settings in appsettings.json.
    /// </summary>
    public const string AppsettingsKey = "UsersDiagnosticsCollector";

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