using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.Basics;
using MassTransit;

namespace EventBus.Infrastructure.Publishers;

internal class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event,
                                           CancellationToken cancellationToken) where TEvent : EventBase
        => await publishEndpoint.Publish(@event, cancellationToken);
}