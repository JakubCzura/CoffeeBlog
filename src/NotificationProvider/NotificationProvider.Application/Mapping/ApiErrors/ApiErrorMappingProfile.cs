using AutoMapper;
using NotificationProvider.Application.Commands.ApiErrors.CreateApiError;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Application.Mapping.ApiErrors;

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