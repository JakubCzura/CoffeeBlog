using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using NotificationProvider.Application.Interfaces.Email;
using NotificationProvider.Domain.Constants;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Enums;
using NotificationProvider.Domain.Models.Emails;
using NotificationProvider.Domain.SettingsOptions.Email;

namespace NotificationProvider.Infrastructure.Email;

internal class EmailSender(IOptions<EmailOptions> _emailOptions) : IEmailSender
{
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

    public async Task<SendEmailMessageResult> SendEmailAsync(EmailMessage emailMessage,
                                                             CancellationToken cancellationToken)
    {
        try
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
            await smtp.SendAsync(emailToSend, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);

            return new SendEmailMessageResult(emailMessage, EmailMessageStatus.Sent);
        }
        catch (SslHandshakeException sslHandshakeException)
        {
            return new SendEmailMessageResult(emailMessage, EmailMessageStatus.ConfigurationError, SmtpErrorConstants.ConnectionError, sslHandshakeException.Message[..300]);
        }
        catch (AuthenticationException authenticationException)
        {
            return new SendEmailMessageResult(emailMessage, EmailMessageStatus.AuthenticationError, SmtpErrorConstants.AuthenticationError, authenticationException.Message[..300]);
        }
        catch (ServiceNotAuthenticatedException serviceNotAuthenticatedException)
        {
            return new SendEmailMessageResult(emailMessage, EmailMessageStatus.AuthenticationError, SmtpErrorConstants.AuthenticationError, serviceNotAuthenticatedException.Message[..300]);
        }
        catch (Exception exception)
        {
            return new SendEmailMessageResult(emailMessage, EmailMessageStatus.ServerError, SmtpErrorConstants.ServerError, exception.Message[..300]);
        }
    }
}