namespace AuthService.Domain.Constants;

/// <summary>
/// Constants for SQL for example when configuring database require to use some SQL code.
/// </summary>
public static class SqlConstants
{
    /// <summary>
    /// Returns current date and time in UTC.
    /// </summary>
    public const string GetUtcDate = "GETUTCDATE()";
}