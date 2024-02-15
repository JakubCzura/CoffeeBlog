using CoffeeBlog.Application.Interfaces.Helpers;

namespace CoffeeBlog.Infrastructure.Helpers;

/// <summary>
/// Delivers default implementation for time and date properties.
/// </summary>
public class DateTimeHelper : IDateTimeHelper
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => DateTime.Now;
}