using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.AuthService.Users;

public record PasswordResetedEvent(string Email,
                                   string Username,
                                   string EventPublisherName) : AuthServiceEventBase(EventPublisherName);