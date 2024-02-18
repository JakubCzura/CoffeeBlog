namespace CoffeeBlog.Domain.Models.Email;

public record PasswordResetEmailMessage(string? To, string? Subject, string? Body) : IEmailMessage;