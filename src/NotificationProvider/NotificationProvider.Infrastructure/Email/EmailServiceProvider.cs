using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Domain.Models.Emails;
using NotificationProvider.Domain.SettingsOptions.Email;

namespace NotificationProvider.Infrastructure.Email;

internal class EmailServiceProvider(IOptions<EmailOptions> _emailOptions) : IEmailServiceProvider
{
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

    public async Task<string> SendEmailAsync(IEmailMessage emailMessage,
                                             CancellationToken cancellationToken)
    {
        MimeMessage emailToSend = new()
        {
            Sender = new MailboxAddress(_emailOptions.CoffeeBlog.SenderName, _emailOptions.CoffeeBlog.Email)
        };
        emailToSend.To.Add(MailboxAddress.Parse(emailMessage.RecipientEmail));
        emailToSend.Subject = emailMessage.Subject;
        emailToSend.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

        using SmtpClient smtp = new();
        await smtp.ConnectAsync(_emailOptions.Smtp.Host, _emailOptions.Smtp.Port, _emailOptions.Smtp.SecureSocketOptions, cancellationToken);
        await smtp.AuthenticateAsync(_emailOptions.CoffeeBlog.Email, _emailOptions.CoffeeBlog.Password, cancellationToken);
        string result = await smtp.SendAsync(emailToSend, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);

        return result;
    }
}