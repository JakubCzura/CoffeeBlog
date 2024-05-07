using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using StatisticsCollector.Application.Consumers.Basics;
using StatisticsCollector.Application.Interfaces.Persistence.Repositories;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Consumers.CommonEvents;

internal sealed class RequestDetailCreatedEventConsumer(ILogger<RequestDetailCreatedEventConsumer> _logger,
                                                        IEventConsumerDetailRepository _eventConsumerDetailRepository,
                                                        IApiErrorRepository _apiErrorRepository,
                                                        IRequestDetailRepository _requestDetailRepository,
                                                        IMapper _mapper)
    : EventConsumerBase<RequestDetailCreatedEvent, RequestDetailCreatedEventConsumer>(_logger, _eventConsumerDetailRepository, _apiErrorRepository)
{
    private readonly IRequestDetailRepository _requestDetailRepository = _requestDetailRepository;
    private readonly IMapper _mapper = _mapper;

    public override async Task ConsumeEvent(ConsumeContext<RequestDetailCreatedEvent> consumeContext)
    {
        RequestDetail requestDetail = _mapper.Map<RequestDetail>(consumeContext.Message);

        await _requestDetailRepository.CreateAsync(requestDetail, default);
    }
}