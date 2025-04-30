namespace NotificationProvider.Domain.Enums;

public enum EmailMessageStatus
{
    Queued = 1,
    Sent = 2,
    ConfigurationError = 3,
    AuthenticationError = 4,
    ServerError = 5
}