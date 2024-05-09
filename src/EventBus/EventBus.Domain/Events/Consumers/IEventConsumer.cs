using EventBus.Domain.Events.Basics;
using MassTransit;

namespace EventBus.Domain.Events.Consumers;

/// <summary>
/// Interface for event consumer.
/// </summary>
public interface IEventConsumer<in T> : IConsumer<T> where T : EventBase
{
}