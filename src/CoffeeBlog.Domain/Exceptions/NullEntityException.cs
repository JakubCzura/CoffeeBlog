namespace CoffeeBlog.Domain.Exceptions;

public class NullEntityException(string? message) : Exception(message)
{
}