using StatisticsCollector.Domain.Entities.Basics;

namespace StatisticsCollector.Domain.Entities;

public class UsersDiagnostics : DbEntityBase
{
    public int NewUserCount { get; set; }
    public int ActiveAccountCount { get; set; }
    public int BannedAccountCount { get; set; }
    public string MostCommonBanReason { get; set; } = string.Empty;
    public int UserWhoLoggedInCount { get; set; }
    public int UserWhoFailedToLogInCount { get; set; }
    public int UserWhoChangedUsernameCount { get; set; }
    public int UserWhoChangedEmailCount { get; set; }
    public int UserWhoChangedPasswordCount { get; set; }

    /// <summary>
    /// Date that represents when the data was collected.
    /// Preferred way to collect data is to use quartz next day to collect data for the previous day.
    /// I care just about the date, not the time, but due to problem with DateOnly property in MongoDB this property is declared as DateTime.
    /// </summary>
    public DateTime DataCollectedAt { get; set; } = DateTime.UtcNow;
}