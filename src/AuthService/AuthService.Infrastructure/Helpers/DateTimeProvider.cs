using AuthService.Application.Interfaces.Helpers;

namespace AuthService.Infrastructure.Helpers;

/// <summary>
/// Delivers default implementation for time and date properties.
/// </summary>
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;
}