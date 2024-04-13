using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.Users;

public class UserSignedUpEvent : EventBase
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}