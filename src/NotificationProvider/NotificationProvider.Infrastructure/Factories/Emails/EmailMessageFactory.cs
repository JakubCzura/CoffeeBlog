using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Infrastructure.Factories.Emails;

internal class EmailMessageFactory : IEmailMessageFactory
{
    public IEmailMessage CreateWelcomeEmailMessage(string to,
                                                   string username)
        => new WelcomeEmailMessage(to,
                                   "Welcome to CoffeeBlog",
                                   $"Hello {username}! Nice to see you! We hope you enjoy drinking coffee.");

    public IEmailMessage CreatePasswordResetEmailMessage(string to,
                                                         string username,
                                                         string token,
                                                         DateTime expirationDate)
        => new PasswordResetEmailMessage(to,
                                        "Reset your password",
                                        $"Hello {username}! You can reset your password using this token: {token}. The token will expire {expirationDate}");
}