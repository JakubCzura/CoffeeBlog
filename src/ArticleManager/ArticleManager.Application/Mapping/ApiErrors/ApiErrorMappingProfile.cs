using ArticleManager.Application.Commands.ApiErrors.CreateApiError;
using ArticleManager.Domain.Entities;
using AutoMapper;

namespace ArticleManager.Application.Mapping.ApiErrors;

/// <summary>
/// Mapping profile for <see cref="ApiError"/>.
/// </summary>
public class ApiErrorMappingProfile 
    : Profile
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