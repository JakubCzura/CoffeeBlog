using EventBus.Domain.Events.Basics;

namespace EventBus.Application.Interfaces.Publishers;

/// <summary>
/// Interface for event publisher. Events allow communication between microservices.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes event to the event bus. The event can be consumed by microservices.
    /// </summary>
    /// <typeparam name="TEvent">Type of event that will be send to queue.</typeparam>
    /// <param name="event">Event that shares information between microservices.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Task"/></returns>
    Task PublishAsync<TEvent>(TEvent @event,
                              CancellationToken cancellationToken) where TEvent : EventBase;
}