using StatisticsCollector.Domain.Entities.Basics;

namespace StatisticsCollector.Domain.Entities;

public class UserDiagnostic : DbEntityBase
{
    public int ActiveAccountCount { get; set; }

    public int BannedAccountCount { get; set; }

    public int FailedSignInAttemptCount { get; set; }

    public int SuccessfulSignInAttemptCount { get; set; }

    /// <summary>
    /// Date that represents when the data was collected.
    /// Preferred way to collect data is to use quartz next day to collect data for the previous day.
    /// </summary>
    public DateOnly DataCollectedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));
}