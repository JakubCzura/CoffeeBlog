using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.Users;

public record UserSignedUpEvent(string Username,
                                string Email) : EventBase();
