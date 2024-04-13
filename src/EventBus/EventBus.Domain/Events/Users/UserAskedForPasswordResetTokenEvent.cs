using EventBus.Domain.Events.Basics;

namespace EventBus.Domain.Events.Users;

public record UserAskedForPasswordResetTokenEvent(string Email,
                                                  string Username,
                                                  string Token,
                                                  DateTime ExpirationDate) : EventBase();
