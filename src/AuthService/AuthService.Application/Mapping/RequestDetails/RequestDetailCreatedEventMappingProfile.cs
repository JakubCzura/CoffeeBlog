using AuthService.Domain.Commands.RequestDetails;
using AutoMapper;
using EventBus.Domain.Events.AuthService.RequestDetails;

namespace AuthService.Application.Mapping.RequestDetails;

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
            .ForCtorParam(nameof(RequestDetailCreatedEvent.EventPublisherName), opt => opt.MapFrom((src, context) => context.Items[nameof(RequestDetailCreatedEvent.EventPublisherName)]));
    }
}