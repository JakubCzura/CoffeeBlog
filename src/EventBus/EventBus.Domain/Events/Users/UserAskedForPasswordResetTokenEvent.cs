using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.Users;

public class UserAskedForPasswordResetTokenEvent : EventBase
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
}