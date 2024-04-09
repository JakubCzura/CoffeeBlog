namespace AuthService.Application.ExtensionMethods.String;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the first character of a string to lowercase.
    /// </summary>
    /// <param name="value">String to convert.</param>
    /// <returns>Converted string with first letter as lowercase.</returns>
    public static string ToCamelCase(this string? value)
        => string.IsNullOrWhiteSpace(value) ? string.Empty : char.ToLowerInvariant(value[0]) + value[1..];
}