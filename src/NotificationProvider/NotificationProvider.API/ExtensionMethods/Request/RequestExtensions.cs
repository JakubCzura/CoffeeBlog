namespace NotificationProvider.API.ExtensionMethods.Request;

/// <summary>
/// Extension methods helping to process a request.
/// </summary>
public static class RequestExtensions
{
    /// <summary>
    /// Reads the body of request or response as a string
    /// </summary>
    /// <param name="body">Stream as request or response</param>
    /// <returns>Body of request or response</returns>
    public static async Task<string> ReadAsStringAsync(this Stream body)
        => await new StreamReader(body).ReadToEndAsync();
}