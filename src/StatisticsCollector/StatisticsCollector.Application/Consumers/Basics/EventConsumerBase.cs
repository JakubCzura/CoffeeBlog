﻿using EventBus.Domain.Events.Basics;
using EventBus.Domain.Events.Consumers;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Consumers.Basics;

internal abstract class EventConsumerBase<TEvent, TEventConsumer>(ILogger<TEventConsumer> _logger,
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
                EventConsumerName = typeof(TEventConsumer).Name,
                EventMessage = context.Message.ToJson(),
            }, default);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error while consuming event.");

            try
            {
                await _apiErrorRepository.CreateAsync(new ApiError
                {
                    Name = (exception).GetType().Name,
                    Exception = exception.ToString(),
                    Message = exception.Message,
                    Description = "Error while consuming event."
                }, default);
            }
            catch (Exception)
            {
                _logger.LogCritical(exception, $"{nameof(EventConsumerBase<TEvent, TEventConsumer>)}: Exception while saving API exception's data to database.");
            }

            await Task.CompletedTask;
        }
    }

    public async Task Consume(ConsumeContext<TEvent> context)
        => await HandleEventConsuming(context);

    public abstract Task ConsumeEvent(ConsumeContext<TEvent> consumeContext);
}