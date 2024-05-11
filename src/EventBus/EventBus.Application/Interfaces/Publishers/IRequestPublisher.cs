using EventBus.Domain.Events.Basics;
using EventBus.Domain.Responses.Basics;
using MassTransit;

namespace EventBus.Application.Interfaces.Publishers;

public interface IRequestPublisher<TResponseRequest> where TResponseRequest : EventBase
{
    Task<Response<TResponse>> GetResponseAsync<TRequest, TResponse>(TRequest request,
                                                                    CancellationToken cancellationToken) where TRequest : EventBase
                                                                                                         where TResponse : ResponseBase;
}