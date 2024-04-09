using AutoMapper;
using AuthService.Domain.Commands.RequestDetails;
using AuthService.Domain.Entities;

namespace AuthService.Application.Mapping.RequestDetails;

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
        CreateMap<CreateRequestDetailCommand, RequestDetail>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SentAt, opt => opt.Ignore());
    }
}