using Microsoft.Extensions.Options;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Helpers;
using NotificationProvider.Domain.Models.Emails;
using NotificationProvider.Domain.SettingsOptions.Email;
using System.Text;

namespace NotificationProvider.Infrastructure.Factories.Emails;

internal class EmailMessageFactory(IOptions<EmailOptions> _emailOptions, 
                                   IDateTimeProvider dateTimeProvider) : IEmailMessageFactory
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

    public string CreateContactUsBody(string recipientName,
                                      string recipientSurname,
                                      string recipientEmail,
                                      string message)
    {
        StringBuilder emailBody = new();
        DateTime currentDate = dateTimeProvider.Now;

        emailBody.AppendLine("<!DOCTYPE html>");
        emailBody.AppendLine("<html>");
        emailBody.AppendLine("<head>");
        emailBody.AppendLine("  <meta charset=\"UTF-8\">");
        emailBody.AppendLine("  <title>Contact Form</title>");
        emailBody.AppendLine("</head>");
        emailBody.AppendLine("<body style=\"margin:0; padding:0; font-family:Arial, sans-serif; background-color:#f4f4f4;\">");
        emailBody.AppendLine("  <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"max-width:600px; margin:auto; background-color:#ffffff; padding:20px; border:1px solid #dddddd;\">");
        emailBody.AppendLine("    <tr>");
        emailBody.AppendLine("      <td align=\"center\" style=\"padding:10px;\">");
        emailBody.AppendLine("        <h2 style=\"color:#333333;\">Coffee Blog Contact Form</h2>");
        emailBody.AppendLine("      </td>");
        emailBody.AppendLine("    </tr>");
        emailBody.AppendLine("    <tr>");
        emailBody.AppendLine("      <td style=\"padding:20px 10px; color:#555555; font-size:16px;\">");
        emailBody.AppendLine($"        <p>Name: {recipientName}</p>");
        emailBody.AppendLine($"        <p>Last name: {recipientSurname}</p>");
        emailBody.AppendLine($"        <p>E-mail: {recipientEmail}</p>");
        emailBody.AppendLine("        <br/>");
        emailBody.AppendLine($"        <p>On {currentDate.ToString("dd.MM.yyyy HH:mm")}, you wrote to us:</p>");
        emailBody.AppendLine($"        <p>{message}</p>");
        emailBody.AppendLine("        <br/>");
        emailBody.AppendLine("        <p>Thank you for contacting us. We appreciate your feedback. Our staff will reply to the message soon.</p>");
        emailBody.AppendLine($"        <p>If you have any questions, feel free to <a href=\"mailto:{_emailOptions.CoffeeBlog.Email}\" style=\"color:#007BFF;\">contact us</a>.</p>");
        emailBody.AppendLine("        <p>Best regards,<br><strong>Coffee Blog Team</strong></p>");
        emailBody.AppendLine("      </td>");
        emailBody.AppendLine("    </tr>");
        emailBody.AppendLine("    <tr>");
        emailBody.AppendLine("      <td align=\"center\" style=\"padding:10px; background-color:#f4f4f4; font-size:12px; color:#999999;\">");
        emailBody.AppendLine($"        {currentDate.Year} Coffee Blog. Thank you for visiting our webpage.");
        emailBody.AppendLine("      </td>");
        emailBody.AppendLine("    </tr>");
        emailBody.AppendLine("  </table>");
        emailBody.AppendLine("</body>");
        emailBody.AppendLine("</html>");

        return emailBody.ToString();
    }     
}