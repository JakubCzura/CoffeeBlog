namespace EventBus.Domain.Responses.Basics;

/// <summary>
/// Base class for requests' responses to communicate between microservices.
/// </summary>
/// <param name="RequestId">Unique identifier of the request.</param>
/// <param name="ResponsePublisherName">Name of the response publisher.</param>
/// <param name="ResponsePublisherMicroserviceName">Name of the microservice that contains publisher of the response.</param>
public abstract record ResponseBase(Guid RequestId,
                                    string ResponsePublisherName,
                                    string ResponsePublisherMicroserviceName)
{
    /// <summary>
    /// Unique identifier of the response.
    /// </summary>
    public Guid ResponseId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Date and time when the response was sent.
    /// </summary>
    public DateTime ResponsePublishedAt { get; set; } = DateTime.UtcNow;
}