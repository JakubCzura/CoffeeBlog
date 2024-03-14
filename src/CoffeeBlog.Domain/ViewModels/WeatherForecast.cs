namespace CoffeeBlog.Domain.ViewModels;

/// <summary>
/// View model for testing purposes. It will be removed in the future.
/// </summary>
[Obsolete("This class is for testing purposes only. It will be removed in the future.")]
public class WeatherForecast
{
    /// <summary>
    /// For tests.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// For tests.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// For tests.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// For tests.
    /// </summary>
    public string? Summary { get; set; }
}