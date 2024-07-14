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
/// <param name="_logger">Logger to log exceptions.</param>
/// <param name="_eventConsumerDetailRepository">Interface to perform event consumer detail operations in database.</param>
/// <param name="_apiErrorRepository">Interface to perform api error operations in database.</param>
/// <param name="_requestDetailRepository">Interface to perform request detail operations in database.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public sealed class RequestDetailCreatedEventConsumer(ILogger<RequestDetailCreatedEventConsumer> _logger,
                                                      IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                      IApiErrorRepository _apiErrorRepository,
                                                      IRequestDetailRepository _requestDetailRepository,
                                                      IMapper _mapper)
    : EventConsumerBase<RequestDetailCreatedEvent, RequestDetailCreatedEventConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IRequestDetailRepository _requestDetailRepository = _requestDetailRepository;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Consumes <see cref="RequestDetailCreatedEvent"/> event.<br/>
    /// Saves request's details to database.
    /// </summary>
    /// <param name="consumeContext">Event's context.</param>
    /// <returns><see cref="Task"/>.</returns>
    public override async Task ConsumeEvent(ConsumeContext<RequestDetailCreatedEvent> consumeContext)
    {
        RequestDetail requestDetail = _mapper.Map<RequestDetail>(consumeContext.Message);

        await _requestDetailRepository.CreateAsync(requestDetail, default);
    }
}