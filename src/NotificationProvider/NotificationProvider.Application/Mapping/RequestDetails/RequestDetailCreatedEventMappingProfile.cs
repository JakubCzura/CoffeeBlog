using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using NotificationProvider.Application.Commands.RequestDetails.CreateRequestDetail;

namespace NotificationProvider.Application.Mapping.RequestDetails;

/// <summary>
/// Mapping profile for <see cref="RequestDetailCreatedEvent"/>.
/// </summary>
public class RequestDetailCreatedEventMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public RequestDetailCreatedEventMappingProfile()
    {
        CreateMap<CreateRequestDetailCommand, RequestDetailCreatedEvent>()
            .ForMember(dest => dest.EventId, opt => opt.Ignore())
            .ForMember(dest => dest.EventPublishedAt, opt => opt.Ignore())
            .ForCtorParam(nameof(RequestDetailCreatedEvent.EventPublisherName), opt => opt.MapFrom((src, context) => context.Items[nameof(RequestDetailCreatedEvent.EventPublisherName)]))
            .ForCtorParam(nameof(RequestDetailCreatedEvent.EventPublisherMicroserviceName), opt => opt.MapFrom((src, context) => context.Items[nameof(RequestDetailCreatedEvent.EventPublisherMicroserviceName)]));
    }
}