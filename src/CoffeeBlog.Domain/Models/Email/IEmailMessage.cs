namespace CoffeeBlog.Domain.Models.Email;

public interface IEmailMessage
{
    string To { get; init; }
    string? Subject { get; init; }
    string? Body { get; init; }
}