namespace EventBus.Domain.Events.Basics;

public abstract record StatisticsCollectorEventBase(string EventPublisherName) : EventBase(EventPublisherName, "StatisticsCollector");