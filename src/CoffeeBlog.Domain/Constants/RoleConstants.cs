namespace AuthService.Domain.Constants;

/// <summary>
/// Constants for roles needed for authorization purposes like Admin etc.
/// </summary>
public static class RoleConstants
{
    /// <summary>
    /// Owner role.
    /// </summary>
    public const string Owner = "Owner";

    /// <summary>
    /// Producer role - company which delivers CoffeeBlog
    /// </summary>
    public const string Producer = "Producer";

    /// <summary>
    /// Admin role.
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Moderator role.
    /// </summary>
    public const string Moderator = "Moderator";

    /// <summary>
    /// User role.
    /// </summary>
    public const string User = "User";
}