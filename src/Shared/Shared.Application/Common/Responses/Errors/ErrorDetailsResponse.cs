using Shared.Application.Common.Responses.Basics;
using System.Text.Json;

namespace Shared.Application.Common.Responses.Errors;

/// <summary>
/// Response for HTTP request when an error occurs while processing a request.
/// </summary>
public class ErrorDetailsResponse : ResponseBase
{
    /// <summary>
    /// Response status code.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ErrorDetailsResponse() : base()
    {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="statusCode">Response status code.</param>
    /// <param name="message">Response from request.</param>
    public ErrorDetailsResponse(int statusCode, 
                                string message) 
        : base(message) => StatusCode = statusCode;

    /// <summary>
    /// Converts the reponse to json.
    /// </summary>
    /// <returns>Response as json.</returns>
    public override string ToString()
        => JsonSerializer.Serialize(this, GetType());
}