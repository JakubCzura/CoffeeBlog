﻿namespace NotificationProvider.Domain.Constants;

/// <summary>
/// Constants for routing.
/// </summary>
public class RouteConstants
{
    /// <summary>
    /// Base route for API controllers.
    /// </summary>
    public const string ApiController = "api/v{version:apiVersion}/[controller]";
}