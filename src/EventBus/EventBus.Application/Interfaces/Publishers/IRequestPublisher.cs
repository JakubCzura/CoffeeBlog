using EventBus.Domain.Events.Basics;
using EventBus.Domain.Responses.Basics;
using MassTransit;

namespace EventBus.Application.Interfaces.Publishers;

public interface IRequestPublisher<TRequest> where TRequest : EventBase
{
    Task<Response<TResponse>> GetResponseAsync<TResponse>(TRequest request,
                                                          CancellationToken cancellationToken) where TResponse : ResponseBase;
}