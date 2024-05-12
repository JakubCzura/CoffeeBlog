using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.Basics;
using EventBus.Domain.Responses.Basics;
using MassTransit;

namespace EventBus.Infrastructure.Publishers;

internal class RequestPublisher<TRequest>(IRequestClient<TRequest> _requestClient)
    : IRequestPublisher<TRequest> where TRequest : EventBase
{
    private readonly IRequestClient<TRequest> _requestClient = _requestClient;

    public async Task<Response<TResponse>> GetResponseAsync<TResponse>(TRequest request,
                                                                       CancellationToken cancellationToken) where TResponse : ResponseBase
        => await _requestClient.GetResponse<TResponse>(request, cancellationToken);
}

