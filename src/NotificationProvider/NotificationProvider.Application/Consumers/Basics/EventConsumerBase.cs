using EventBus.Domain.Events.Basics;
using EventBus.Domain.Events.Consumers;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Consumers.Basics;

/// <summary>
/// Base class for event's consumers that logs and saves consumed events and exceptions to database.
/// It consumes messages according to microservices communication.
/// </summary>
/// <typeparam name="TEvent">Type of event.</typeparam>
/// <typeparam name="TEventConsumer">Type of event's consumer that will inherit from this class.</typeparam>
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="_apiErrorRepository">Interface to perform api error operations in database.</param>
public abstract class EventConsumerBase<TEvent, TEventConsumer>(ILogger<TEventConsumer> _logger,
                                                                IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                                IApiErrorRepository _apiErrorRepository)
    : IEventConsumer<TEvent> where TEvent : EventBase
                             where TEventConsumer : IEventConsumer<TEvent>
{
    private readonly ILogger<TEventConsumer> _logger = _logger;
    private readonly IEventConsumerDetailRepository _eventConsumerDetailRepository = _eventConsumerDetailRepository;
    private readonly IApiErrorRepository _apiErrorRepository = _apiErrorRepository;

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
            await _eventConsumerDetailRepository.CreateAsync(new EventConsumerDetail
            {
                EventId = context.Message.EventId,
                EventPublishedAt = context.Message.EventPublishedAt,
                EventName = typeof(TEvent).Name,
                EventPublisherName = context.Message.EventPublisherName,
                EventPublisherMicroserviceName = context.Message.EventPublisherMicroserviceName,
                EventConsumerName = typeof(TEventConsumer).Name,
                EventMessage = context.Message.ToJson(),
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
            _logger.LogError(eventHandlerException, "Error while consuming event.");
            await _apiErrorRepository.CreateAsync(new ApiError
            {
                Name = (eventHandlerException).GetType().Name,
                Exception = eventHandlerException.ToString(),
                Message = eventHandlerException.Message,
                Description = "Error while consuming event."
            }, default);
        }
        catch (Exception loggerException)
        {
            _logger.LogCritical(loggerException, $"{nameof(EventConsumerBase<TEvent, TEventConsumer>)}: Exception while saving API exception's data to database.");
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