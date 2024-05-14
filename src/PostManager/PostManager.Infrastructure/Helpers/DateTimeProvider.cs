using PostManager.Application.Interfaces.Helpers;

namespace PostManager.Infrastructure.Helpers;

/// <summary>
/// Delivers default implementation for time and date properties.
/// </summary>
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;
}