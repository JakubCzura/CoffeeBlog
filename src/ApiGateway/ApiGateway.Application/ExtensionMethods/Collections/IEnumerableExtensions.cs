﻿namespace ApiGateway.Application.ExtensionMethods.Collections;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Checks if any element in the collection is null, empty or whitespace.
    /// </summary>
    /// <param name="elements">Collection of strings.</param>
    /// <returns>True if any element in the collection is null, empty or whitespace, otherwise false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when collection is null.</exception>"
    public static bool IsAnyElementNullOrWhiteSpace(this IEnumerable<string?> elements)
        => elements.Any(string.IsNullOrWhiteSpace);
}