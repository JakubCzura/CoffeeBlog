using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using PostManager.Application.Commands.RequestDetails.CreateRequestDetail;

namespace PostManager.Application.ExtensionMethods.Automapper.Events;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperForRequestDetailCreatedEventExtensions
{
    /// <summary>
    /// Maps <see cref="CreateRequestDetailCommand"/> to <see cref="RequestDetailCreatedEvent"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="RequestDetailCreatedEvent"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="createRequestDetailCommand">CreateRequestDetailCommand entity.</param>
    /// <param name="eventPublisherName">Name of event publisher.</param>
    /// <param name="eventPublisherMicroserviceName">Name of microservice that contains publisher of the event.</param>
    /// <returns>Instance of <see cref="RequestDetailCreatedEvent"/></returns>
    public static RequestDetailCreatedEvent Map<T>(this IMapper mapper,
                                                   CreateRequestDetailCommand createRequestDetailCommand,
                                                   string eventPublisherName,
                                                   string eventPublisherMicroserviceName) where T : RequestDetailCreatedEvent
        => mapper.Map<RequestDetailCreatedEvent>(createRequestDetailCommand, opt =>
        {
            opt.Items[nameof(RequestDetailCreatedEvent.EventPublisherName)] = eventPublisherName;
            opt.Items[nameof(RequestDetailCreatedEvent.EventPublisherMicroserviceName)] = eventPublisherMicroserviceName;
        });
}