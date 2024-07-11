using EventBus.Domain.Events.Basics;
using EventBus.Domain.Events.Consumers;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Consumers.Basics;

public abstract class EventConsumerBase<TEvent, TEventConsumer>(ILogger<TEventConsumer> _logger,
                                                                  IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                                  IApiErrorRepository _apiErrorRepository)
    : IEventConsumer<TEvent> where TEvent : EventBase
                             where TEventConsumer : IEventConsumer<TEvent>
{
    private readonly ILogger<TEventConsumer> _logger = _logger;
    private readonly IEventConsumerDetailRepository _eventConsumerDetailRepository = _eventConsumerDetailRepository;
    private readonly IApiErrorRepository _apiErrorRepository = _apiErrorRepository;

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
    /// <returns><see cref="Task"/></returns>
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
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Consume(ConsumeContext<TEvent> context)
        => await HandleEventConsuming(context);

    /// <summary>
    /// Method to consume event that should be inherited and overridden.
    /// </summary>
    /// <param name="consumeContext">Event's context.</param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public abstract Task ConsumeEvent(ConsumeContext<TEvent> consumeContext);
}