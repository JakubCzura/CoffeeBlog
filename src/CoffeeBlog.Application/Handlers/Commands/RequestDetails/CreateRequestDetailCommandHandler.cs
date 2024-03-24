using AutoMapper;
using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Domain.Commands.RequestDetails;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Resources;
using CoffeeBlog.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace CoffeeBlog.Application.Handlers.Commands.RequestDetails;

/// <summary>
/// Command handler to create new request's details and save it to database. It's related to <see cref="CreateRequestDetailCommand"/>.
/// </summary>
/// <param name="_requestDetailRepository">Interface to perform request's details operations in database.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class CreateRequestDetailCommandHandler(IRequestDetailRepository _requestDetailRepository,
                                               IMapper _mapper) : IRequestHandler<CreateRequestDetailCommand, Result<ViewModelBase>>
{
    private readonly IRequestDetailRepository _requestDetailRepository = _requestDetailRepository;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Handles request to create new request's details and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create request's details.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Instance of <see cref="ViewModelBase"/></returns>
    public async Task<Result<ViewModelBase>> Handle(CreateRequestDetailCommand request,
                                                    CancellationToken cancellationToken)
    {
        RequestDetail requestDetail = _mapper.Map<RequestDetail>(request);
        
        await _requestDetailRepository.CreateAsync(requestDetail, cancellationToken);

        ViewModelBase result = new(ResponseMessages.RequestDetailsHaveBeenSaved);

        return Result.Ok(result);
    }
}