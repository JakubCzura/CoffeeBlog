using AutoMapper;
using MediatR;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Domain.Entities;

namespace PostManager.Application.Commands.ApiErrors.CreateApiError;

/// <summary>
/// Command handler to create new API error and save it to database. It's related to <see cref="CreateApiErrorCommand"/>.
/// </summary>
/// <param name="_apiErrorRepository">Interface to perform API error's operations in database.</param>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class CreateApiErrorCommandHandler(IApiErrorRepository _apiErrorRepository,
                                          IMapper _mapper)
    : IRequestHandler<CreateApiErrorCommand, Unit>
{
    private readonly IApiErrorRepository _apiErrorRepository = _apiErrorRepository;
    private readonly IMapper _mapper = _mapper;

    /// <summary>
    /// Handles request to create new API error and save it to database.
    /// </summary>
    /// <param name="request">Request command with details to create API error.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns><see cref="Unit.Value"/></returns>
    public async Task<Unit> Handle(CreateApiErrorCommand request,
                                   CancellationToken cancellationToken)
    {
        ApiError apiError = _mapper.Map<ApiError>(request);
        await _apiErrorRepository.CreateAsync(apiError, cancellationToken);

        return Unit.Value;
    }
}