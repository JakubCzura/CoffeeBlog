using Contracts.Events.Basics;

namespace Contracts.Events.Users;

public class UserSignedUpEvent : EventBase
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}