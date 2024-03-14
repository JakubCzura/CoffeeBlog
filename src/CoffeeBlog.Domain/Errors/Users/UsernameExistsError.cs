using FluentResults;

namespace CoffeeBlog.Domain.Errors.Users;

public class UsernameExistsError(string message) : Error(message)
{
}