using AuthService.Domain.Entities;
using AuthService.Domain.ViewModels.Users;
using AutoMapper;

namespace AuthService.Application.Mapping.Users;

/// <summary>
/// Mapping profile for <see cref="SignUpUserViewModel"/>.
/// </summary>
public class SignUpUserViewModelMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public SignUpUserViewModelMappingProfile()
    {
        CreateMap<User, SignUpUserViewModel>()
            .ForCtorParam(nameof(SignUpUserViewModel.UserId), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(SignUpUserViewModel.Username), opt => opt.MapFrom(src => src.Username))
            .ForCtorParam(nameof(SignUpUserViewModel.Email), opt => opt.MapFrom(src => src.Email))
            .ForCtorParam(nameof(SignUpUserViewModel.JwtToken), opt => opt.MapFrom((src, context) => context.Items[nameof(SignUpUserViewModel.JwtToken)]));
    }
}