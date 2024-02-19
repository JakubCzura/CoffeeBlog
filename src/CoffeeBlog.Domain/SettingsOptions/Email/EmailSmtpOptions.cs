using MailKit.Security;

namespace CoffeeBlog.Domain.SettingsOptions.Email;

public class EmailSmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.StartTls;
}