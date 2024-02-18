namespace CoffeeBlog.Domain.Models.Email;

public record WelcomeEmailMessage(string? To, string? Subject, string? Body) : IEmailMessage;
