using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.AuthService.Users;

public record PasswordResetTokenSentEvent(string Email,
                                          string Username,
                                          string Token,
                                          DateTime ExpirationDate,
                                          string EventPublisherName) : AuthServiceEventBase(EventPublisherName);