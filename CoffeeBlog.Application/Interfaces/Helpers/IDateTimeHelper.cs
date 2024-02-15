namespace CoffeeBlog.Application.Interfaces.Helpers;

/// <summary>
/// Interface to deliver abstracted time and date properties. It is helpful for testing purposes. Use it in repositories and services which need time provided.
/// </summary>
public interface IDateTimeHelper
{
    public DateTime UtcNow { get; }

    public DateTime Now { get; }
}