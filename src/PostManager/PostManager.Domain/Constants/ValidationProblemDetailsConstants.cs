namespace PostManager.Domain.Constants;

/// <summary>
/// Constants for returning result from controller when validation problems occur.
/// </summary>
public class ValidationProblemDetailsConstants
{
    /// <summary>
    /// Details to decrive bad request type.
    /// </summary>
    public const string BadRequestType = "https://tools.ietf.org/html/rfc9110#section-15.5.1";

    /// <summary>
    /// TraceId key.
    /// </summary>
    public const string TraceId = "traceId";
}