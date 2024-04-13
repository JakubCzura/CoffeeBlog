using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.Basics;
using MassTransit;

namespace EventBus.Infrastructure.Publishers;

internal class EventPublisher(IPublishEndpoint _publishEndpoint) : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint = _publishEndpoint;

    public async Task PublishAsync<TEvent>(TEvent @event,
                                           CancellationToken cancellationToken) where TEvent : EventBase 
        => await _publishEndpoint.Publish(@event, cancellationToken);
}