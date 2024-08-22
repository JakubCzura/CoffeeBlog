using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using StatisticsCollector.Application.Consumers.Basics;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Consumers.CommonEvents;

/// <summary>
/// Consumer of <see cref="RequestDetailCreatedEvent"/> event.
/// </summary>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="apiErrorRepository">Interface to perform api error operations in database.</param>
/// <param name="requestDetailRepository">Interface to perform request detail operations in database.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public sealed class RequestDetailCreatedEventConsumer(ILogger<RequestDetailCreatedEventConsumer> logger,
                                                      IEventConsumerDetailRepository eventConsumerDetailRepository,
                                                      IApiErrorRepository apiErrorRepository,
                                                      IRequestDetailRepository requestDetailRepository,
                                                      IMapper mapper)
    : EventConsumerBase<RequestDetailCreatedEvent, RequestDetailCreatedEventConsumer>(logger, eventConsumerDetailRepository, apiErrorRepository)
{
    /// <summary>
    /// Consumes <see cref="RequestDetailCreatedEvent"/> event.<br/>
    /// Saves request's details to database.
    /// </summary>
    /// <param name="consumeContext">Event's context.</param>
    /// <returns><see cref="Task"/>.</returns>
    public override async Task ConsumeEvent(ConsumeContext<RequestDetailCreatedEvent> consumeContext)
    {
        RequestDetail requestDetail = mapper.Map<RequestDetail>(consumeContext.Message);

        await requestDetailRepository.CreateAsync(requestDetail, default);
    }
}