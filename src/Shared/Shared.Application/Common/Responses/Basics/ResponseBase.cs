using System.Text.Json;

namespace Shared.Application.Common.Responses.Basics;

/// <summary>
/// Base response for HTTP request.
/// </summary>
public class ResponseBase
{
    /// <summary>
    /// Indicates if the request was successful.
    /// </summary>
    public bool IsSuccess { get; set; } = true;

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
    /// <param name="isSuccess">Idicates if the request was successful.</param>
    /// <param name="responseMessage">Response from request.</param>
    public ResponseBase(string? responseMessage, bool isSuccess = true)
    {
        IsSuccess = isSuccess;
        ResponseMessage = responseMessage;
    }

    /// <summary>
    /// Converts the reponse to json.
    /// </summary>
    /// <returns>Response as json.</returns>
    public override string ToString()
        => JsonSerializer.Serialize(this, GetType());
}