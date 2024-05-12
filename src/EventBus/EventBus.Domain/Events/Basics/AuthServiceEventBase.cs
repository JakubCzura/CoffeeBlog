namespace EventBus.Domain.Events.Basics;

public abstract record AuthServiceEventBase(string EventPublisherName) : EventBase(EventPublisherName, "AuthService");