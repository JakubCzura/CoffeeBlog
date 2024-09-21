namespace Shared.Application.Common.Responses.Basics;

/// <summary>
/// Base response for HTTP request.
/// </summary>
public class ResponseBase
{
    /// <summary>
    /// Response message which user gets from request.
    /// </summary>
    public string? ResponseMessage { get; set; } = null;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ResponseBase()
    {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="responseMessage">Response from request.</param>
    public ResponseBase(string? responseMessage) => ResponseMessage = responseMessage;
}