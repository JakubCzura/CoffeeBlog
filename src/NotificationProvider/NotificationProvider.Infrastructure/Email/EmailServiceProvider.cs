using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Domain.Models.Emails;
using NotificationProvider.Domain.SettingsOptions.Email;

namespace NotificationProvider.Infrastructure.Email;

internal class EmailServiceProvider(ILogger<EmailServiceProvider> _logger,
                                    IOptions<EmailOptions> _emailOptions) : IEmailServiceProvider
{
    private readonly ILogger<EmailServiceProvider> _logger = _logger;
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

    public async Task<string> SendEmailAsync(IEmailMessage emailMessage)
    {
        try
        {
            MimeMessage emailToSend = new()
            {
                Sender = new MailboxAddress(_emailOptions.CoffeeBlog.SenderName, _emailOptions.CoffeeBlog.Email)
            };
            emailToSend.To.Add(MailboxAddress.Parse(emailMessage.To));
            emailToSend.Subject = emailMessage.Subject;
            emailToSend.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

            using SmtpClient smtp = new();
            await smtp.ConnectAsync(_emailOptions.Smtp.Host, _emailOptions.Smtp.Port, _emailOptions.Smtp.SecureSocketOptions);
            await smtp.AuthenticateAsync(_emailOptions.CoffeeBlog.Email, _emailOptions.CoffeeBlog.Password);
            string result = await smtp.SendAsync(emailToSend);
            await smtp.DisconnectAsync(true);

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while sending email.");
            return string.Empty;
        }
    }
}