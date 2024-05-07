using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.Mapping.RequetsDetails;

/// <summary>
/// Mapping profile for <see cref="RequestDetail"/>.
/// </summary>
public class RequestDetailMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public RequestDetailMappingProfile()
    {
        CreateMap<RequestDetailCreatedEvent, RequestDetail>()
            .ForMember(dest => dest.MicroserviceName, opt => opt.MapFrom(src => src.EventPublisherMicroserviceName))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForSourceMember(src => src.EventId, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.EventPublishedAt, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.EventPublisherName, opt => opt.DoNotValidate());
    }
}