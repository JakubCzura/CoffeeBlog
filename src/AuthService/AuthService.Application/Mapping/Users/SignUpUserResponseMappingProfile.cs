using AuthService.Domain.Entities;
using AutoMapper;
using Shared.Application.AuthService.Responses.Users;

namespace AuthService.Application.Mapping.Users;

/// <summary>
/// Mapping profile for <see cref="SignUpUserResponse"/>.
/// </summary>
public class SignUpUserResponseMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public SignUpUserResponseMappingProfile()
    {
        CreateMap<User, SignUpUserResponse>()
            .ForCtorParam(nameof(SignUpUserResponse.UserId), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(SignUpUserResponse.JwtToken), opt => opt.MapFrom((src, context) => context.Items[nameof(SignUpUserResponse.JwtToken)]));
    }
}