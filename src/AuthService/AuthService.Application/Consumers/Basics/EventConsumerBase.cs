using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Domain.Entities;
using EventBus.Domain.Events.Basics;
using EventBus.Domain.Events.Consumers;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AuthService.Application.Consumers.Basics;

/// <summary>
/// Base class for event's consumers that logs and saves consumed events and exceptions to database.
/// It consumes messages according to microservices communication.
/// </summary>
/// <typeparam name="TEvent">Type of event.</typeparam>
/// <typeparam name="TEventConsumer">Type of event's consumer that will inherit from this class.</typeparam>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="apiErrorRepository">Interface to perform api error operations in database.</param>
public abstract class EventConsumerBase<TEvent, TEventConsumer>(ILogger<TEventConsumer> logger,
                                                                IEventConsumerDetailRepository eventConsumerDetailRepository,
                                                                IApiErrorRepository apiErrorRepository)
    : IEventConsumer<TEvent> where TEvent : EventBase
                             where TEventConsumer : IEventConsumer<TEvent>
{
    /// <summary>
    /// Handles event consuming.
    /// </summary>
    /// <param name="context">Context of event.</param>
    /// <returns><see cref="Task"/></returns>
    private async Task HandleEventConsuming(ConsumeContext<TEvent> context)
    {
        try
        {
            await ConsumeEvent(context);
            await eventConsumerDetailRepository.CreateAsync(new EventConsumerDetail
            {
                EventId = context.Message.EventId,
                EventPublishedAt = context.Message.EventPublishedAt,
                EventName = typeof(TEvent).Name,
                EventPublisherName = context.Message.EventPublisherName,
                EventPublisherMicroserviceName = context.Message.EventPublisherMicroserviceName,
                EventConsumerName = typeof(TEventConsumer).Name,
                EventMessage = JsonSerializer.Serialize(context.Message),
            }, default);
        }
        catch (RequestFaultException requestFaultException)
        {
            await LogException(requestFaultException);

            //Pass original exception.
            throw;
        }
        catch (Exception exception)
        {
            await LogException(exception);

            //Finish consuming event.
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Logs and saves exception to database.
    /// </summary>
    /// <param name="eventHandlerException">Exception that was thrown by event handler.</param>
    /// <returns><see cref="Task"/>.</returns>
    private async Task LogException(Exception eventHandlerException)
    {
        try
        {
            logger.LogError(eventHandlerException, "Error while consuming event.");
            await apiErrorRepository.CreateAsync(new ApiError
            {
                Name = (eventHandlerException).GetType().Name,
                Exception = eventHandlerException.ToString(),
                Message = eventHandlerException.Message,
                Description = "Error while consuming event."
            }, default);
        }
        catch (Exception loggerException)
        {
            logger.LogCritical(loggerException, $"{nameof(EventConsumerBase<TEvent, TEventConsumer>)}: Exception while saving API exception's data to database.");
        }
    }

    /// <summary>
    /// Method to consume event.
    /// </summary>
    /// <param name="context">Event's context.</param>
    /// <returns><see cref="Task"/>.</returns>
    public async Task Consume(ConsumeContext<TEvent> context)
        => await HandleEventConsuming(context);

    /// <summary>
    /// Method to consume event that should be inherited and overridden.
    /// </summary>
    /// <param name="consumeContext">Event's context.</param>
    /// <returns><see cref="Task"/>.</returns>
    public abstract Task ConsumeEvent(ConsumeContext<TEvent> consumeContext);
}