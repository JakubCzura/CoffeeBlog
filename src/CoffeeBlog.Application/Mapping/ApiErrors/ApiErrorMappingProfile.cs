using AutoMapper;
using AuthService.Domain.Commands.ApiErrors;
using AuthService.Domain.Entities;

namespace AuthService.Application.Mapping.ApiErrors;

/// <summary>
/// Mapping profile for <see cref="ApiError"/>.
/// </summary>
public class ApiErrorMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public ApiErrorMappingProfile()
    {
        CreateMap<CreateApiErrorCommand, ApiError>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ThrownAt, opt => opt.Ignore());
    }
}