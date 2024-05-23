using PostManager.Domain.ViewModels.Basics;
using System.Text.Json;

namespace PostManager.Domain.ViewModels.Errors;

/// <summary>
/// View model when an error occurs while processing a request.
/// </summary>
public class ErrorDetailsViewModel : ViewModelBase
{
    /// <summary>
    /// Response status code.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ErrorDetailsViewModel() : base()
    {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="statusCode">Response status code.</param>
    /// <param name="message">Response from request.</param>
    public ErrorDetailsViewModel(int statusCode, string message) : base(message) => StatusCode = statusCode;

    /// <summary>
    /// Converts the reponse to json.
    /// </summary>
    /// <returns>Response as json.</returns>
    public override string ToString()
        => JsonSerializer.Serialize(this, GetType());
}