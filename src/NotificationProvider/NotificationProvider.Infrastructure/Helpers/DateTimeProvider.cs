using NotificationProvider.Application.Interfaces.Helpers;

namespace NotificationProvider.Infrastructure.Helpers;

/// <summary>
/// Delivers default implementation for time and date properties.
/// </summary>
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;

    public DateOnly FromDateTime(DateTime dateTime) => DateOnly.FromDateTime(dateTime);
}