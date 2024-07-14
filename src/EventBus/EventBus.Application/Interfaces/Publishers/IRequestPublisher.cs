using EventBus.Domain.Events.Basics;
using EventBus.Domain.Responses.Basics;
using MassTransit;

namespace EventBus.Application.Interfaces.Publishers;

/// <summary>
/// Interface for request publisher. Requests allow communication between microservices.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
public interface IRequestPublisher<TRequest> where TRequest : EventBase
{
    /// <summary>
    /// Sends request to the event bus and expects to get response. The request can be consumed by microservices.
    /// </summary>
    /// <typeparam name="TResponse">Type of request that will be send to queue.</typeparam>
    /// <param name="request">Request that shares information between microservices. When consuming this request, microservice should send response.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Response from microservice that consumed request.</returns>
    Task<Response<TResponse>> GetResponseAsync<TResponse>(TRequest request,
                                                          CancellationToken cancellationToken) where TResponse : ResponseBase;
}