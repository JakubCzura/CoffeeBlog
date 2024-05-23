namespace PostManager.Domain.Exceptions;

/// <summary>
/// Exception thrown when an entity is null.
/// </summary>
/// <param name="message">Exception's details.</param>
public class NullEntityException(string? message) : Exception(message)
{
}