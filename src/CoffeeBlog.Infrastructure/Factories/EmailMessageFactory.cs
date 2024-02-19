using CoffeeBlog.Application.Factories.Email;
using CoffeeBlog.Domain.Models.Email;

namespace CoffeeBlog.Infrastructure.Factories;

public class EmailMessageFactory : IEmailMessageFactory
{
    public IEmailMessage CreateWelcomeEmailMessage(string to, string nickname)
        => new WelcomeEmailMessage(to,
                                   "Welcome to CoffeeBlog",
                                   $"Hello {nickname}! Nice to see you! We hope you enjoy drinking coffee.");

    public IEmailMessage CreatePasswordResetEmailMessage(string to, string nickname, string token, DateTime expirationDate)
        => new PasswordResetEmailMessage(to,
                                        "Reset your password",
                                        $"Hello {nickname}! You can reset your password using this token: {token}. The token will expire {expirationDate}");
}