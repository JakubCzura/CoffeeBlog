using EventBus.Domain.Events.Basics;

namespace EventBus.Application.Interfaces.Publishers;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event,
                              CancellationToken cancellationToken) where TEvent : EventBase;
}