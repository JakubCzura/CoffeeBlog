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
                                         recipientName,
                                         recipientEmail,
                                         "Reset your password",
                                         $"Hello {recipientName}! You can reset your password using this token: {token}. The token will expire {expirationDate}");

    public IEmailMessage CreatePasswordResetedEmailMessage(string recipientName,
                                                           string recipientEmail) 
        => new PasswordResetedEmailMessage(_emailOptions.CoffeeBlog.SenderName,
                                           _emailOptions.CoffeeBlog.Email,
                                           recipientName,
                                           recipientEmail,
                                           "You have just reseted your password",
                                           $"Hello {recipientName}! You have just reseted your password. Thanks for paying attention for security and remember not to share your password with anybody.");
}