using CoffeeBlog.Application.Email;
using CoffeeBlog.Domain.Models.Email;
using CoffeeBlog.Domain.SettingsOptions.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace CoffeeBlog.Infrastructure.Email;

internal class EmailServiceProvider(IOptions<EmailOptions> emailOptions) : IEmailServiceProvider
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;

    public async Task<string> SendEmailAsync(IEmailMessage emailMessage)
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
}