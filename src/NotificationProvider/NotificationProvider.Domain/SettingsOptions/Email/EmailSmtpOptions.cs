using MailKit.Security;

namespace NotificationProvider.Domain.SettingsOptions.Email;

/// <summary>
/// Configuration options for SMTP server.
/// </summary>
public class EmailSmtpOptions
{
    /// <summary>
    /// SMTP server host.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// SMTP server port.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Encryption options for secure connection.
    /// </summary>
    public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.StartTls;
}