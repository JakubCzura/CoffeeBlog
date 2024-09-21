using AuthService.Application.ExtensionMethods.Automapper.RequestDetails;
using AuthService.Domain.Constants;
using AutoMapper;
using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.CommonEvents;
using MediatR;
using Shared.Application.AuthService.Commands.RequestDetails.CreateRequestDetail;

namespace AuthService.Application.Commands.RequestDetails.CreateRequestDetail;

/// <summary>
/// Command handler to create new request's details and save it to database. It's related to <see cref="CreateRequestDetailCommand"/>.
/// </summary>
/// <param name="eventPublisher">Microservice to send event about request's details.</param>
/// <param name="mapper">AutoMapper to map classes.</param>
public class CreateRequestDetailCommandHandler(IEventPublisher eventPublisher,
                                               IMapper mapper)
    : IRequestHandler<CreateRequestDetailCommand, Unit>
{
    /// <summary>
    /// Handles request to create new request's details and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create request's details.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Unit.Value"/></returns>
    public async Task<Unit> Handle(CreateRequestDetailCommand request,
                                   CancellationToken cancellationToken)
    {
        RequestDetailCreatedEvent requestDetailCreatedEvent = mapper.Map<RequestDetailCreatedEvent>(request, nameof(CreateRequestDetailCommandHandler), MicroserviceInfoConstants.Name);
        await eventPublisher.PublishAsync(requestDetailCreatedEvent, cancellationToken);

        return Unit.Value;
    }
}