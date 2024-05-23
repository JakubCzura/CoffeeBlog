namespace PostManager.Domain.Constants;

/// <summary>
/// Constants for routing.
/// </summary>
public static class RouteConstants
{
    /// <summary>
    /// Base route for API controllers.
    /// </summary>
    public const string ApiController = "api/v{version:apiVersion}/[controller]";
}