using AutoMapper;
using CoffeeBlog.Application.Handlers.Basics;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Commands.RequestDetails;
using CoffeeBlog.Domain.Entities;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.RequestDetails;

/// <summary>
/// Command handler to create new request's details and save it to database. It's related to <see cref="CreateRequestDetailCommand"/>.
/// </summary>
/// <param name="_requestDetailRepository">Interface to perform request's details operations in database.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class CreateRequestDetailCommandHandler(IRequestDetailRepository _requestDetailRepository,
                                               IMapper _mapper) : CommandHandlerBase(_mapper), IRequestHandler<CreateRequestDetailCommand, Unit>
{
    private readonly IRequestDetailRepository _requestDetailRepository = _requestDetailRepository;

    /// <summary>
    /// Handles request to create new request's details and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create request's details.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Unit.Value"/></returns>
    public async Task<Unit> Handle(CreateRequestDetailCommand request,
                                   CancellationToken cancellationToken)
    {
        RequestDetail requestDetail = Mapper.Map<RequestDetail>(request);

        await _requestDetailRepository.CreateAsync(requestDetail, cancellationToken);

        return Unit.Value;
    }
}