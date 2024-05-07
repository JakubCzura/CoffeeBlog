using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.AuthService.Users;

public record UserSignedUpEvent(string Username,
                                string Email,
                                string EventPublisherName) : AuthServiceEventBase(EventPublisherName);
