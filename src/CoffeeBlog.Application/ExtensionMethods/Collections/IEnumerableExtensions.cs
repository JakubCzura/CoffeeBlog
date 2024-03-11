namespace CoffeeBlog.Application.ExtensionMethods.Collections;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    public static bool IsAnyElementNullOrWhiteSpace(this IEnumerable<string?> elements)
        => elements.Any(string.IsNullOrWhiteSpace);
}