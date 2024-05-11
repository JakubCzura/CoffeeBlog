using EventBus.Application.Interfaces.Publishers;
using EventBus.Domain.Events.Basics;
using EventBus.Domain.Responses.Basics;
using MassTransit;

namespace EventBus.Infrastructure.Publishers;

internal class RequestPublisher<TResponseRequest>(IRequestClient<TResponseRequest> _requestClient)
    : IRequestPublisher<TResponseRequest> where TResponseRequest : EventBase
{
    private readonly IRequestClient<TResponseRequest> _requestClient = _requestClient;

    public async Task<Response<TResponse>> GetResponseAsync<TRequest, TResponse>(TRequest request,
                                                                                 CancellationToken cancellationToken) where TRequest : EventBase
                                                                                                                      where TResponse : ResponseBase
        => await _requestClient.GetResponse<TResponse>(request, cancellationToken);
}