using FluentResults;

namespace CoffeeBlog.Domain.Errors.Users;

public class EmailExistsError(string message) : Error(message);
