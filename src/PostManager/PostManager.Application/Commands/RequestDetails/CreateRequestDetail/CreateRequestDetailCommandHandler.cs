using AutoMapper;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.CommonEvents;
using MediatR;
using PostManager.Domain.Constants;
using PostManager.Application.ExtensionMethods.Automapper.Events;

namespace PostManager.Application.Commands.RequestDetails.CreateRequestDetail;

public class CreateRequestDetailCommandHandler(IEventPublisher _eventPublisher,
                                               IMapper _mapper)
: IRequestHandler<CreateRequestDetailCommand, Unit>
{
    private readonly IEventPublisher _eventPublisher = _eventPublisher;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Handles request to create new request's details and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create request's details.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Unit.Value"/></returns>
    public async Task<Unit> Handle(CreateRequestDetailCommand request,
                                   CancellationToken cancellationToken)
    {
        RequestDetailCreatedEvent requestDetailCreatedEvent = _mapper.Map<RequestDetailCreatedEvent>(request, nameof(CreateRequestDetailCommandHandler), MicroserviceInfoConstants.Name);
        await _eventPublisher.PublishAsync(requestDetailCreatedEvent, cancellationToken);

        return Unit.Value;
    }
}