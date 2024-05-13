using AutoMapper;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;

namespace StatisticsCollector.Application.Mapping.UsersDiagnostics;

/// <summary>
/// Mapping profile for <see cref="Domain.Entities.UsersDiagnostics"/>.
/// </summary>
public class UsersDiagnosticsMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public UsersDiagnosticsMappingProfile()
    {
        CreateMap<GetUsersDiagnosticDataResponse, Domain.Entities.UsersDiagnostics>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForSourceMember(src => src.RequestId, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.ResponsePublisherName, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.ResponsePublisherMicroserviceName, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.ResponseId, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.ResponsePublishedAt, opt => opt.DoNotValidate());
    }
}