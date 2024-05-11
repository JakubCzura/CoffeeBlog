namespace EventBus.Domain.Responses.Basics;

/// <summary>
/// Base class for responses from AuthService to communicate between microservices.
/// </summary>
/// <param name="RequestId">Id of request that will get response.</param>
/// <param name="ResponsePublisherName">Name of publisher that published request.</param>
public abstract record AuthServiceResponseBase(Guid RequestId,
                                               string ResponsePublisherName) : ResponseBase(RequestId, ResponsePublisherName, "AuthService");