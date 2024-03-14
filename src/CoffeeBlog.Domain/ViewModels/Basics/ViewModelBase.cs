namespace CoffeeBlog.Domain.ViewModels.Basics;

/// <summary>
/// Base class for view models.
/// </summary>
public class ViewModelBase
{
    /// <summary>
    /// Response message which user gets from request.
    /// </summary>
    public string? ResponseMessage { get; set; } = null;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ViewModelBase()
    {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="responseMessage">Response from request.</param>
    public ViewModelBase(string? responseMessage) => ResponseMessage = responseMessage;
}