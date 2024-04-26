using Microsoft.Extensions.Options;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Domain.Models.Emails;
using NotificationProvider.Domain.SettingsOptions.Email;

namespace NotificationProvider.Infrastructure.Factories.Emails;

internal class EmailMessageFactory(IOptions<EmailOptions> _emailOptions) : IEmailMessageFactory
{
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

    public IEmailMessage CreateWelcomeEmailMessage(string recipientName,
                                                   string recipientEmail)
        => new WelcomeEmailMessage(_emailOptions.CoffeeBlog.SenderName,
                                   _emailOptions.CoffeeBlog.Email,
                                   recipientName,
                                   recipientEmail,
                                   "Welcome to CoffeeBlog",
                                   $"Hello {recipientName}! Nice to see you! We hope you enjoy drinking coffee.");

    public IEmailMessage CreatePasswordResetEmailMessage(string recipientName,
                                                         string recipientEmail,
                                                         string token,
                                                         DateTime expirationDate)
        => new PasswordResetEmailMessage(_emailOptions.CoffeeBlog.SenderName,
                                         _emailOptions.CoffeeBlog.Email,
                                         recipientEmail,
                                         token,
                                         "Reset your password",
                                         $"Hello {recipientName}! You can reset your password using this token: {token}. The token will expire {expirationDate}");
}