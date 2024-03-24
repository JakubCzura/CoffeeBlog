using AutoMapper;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;

namespace CoffeeBlog.Application.Mapping.Users;

/// <summary>
/// Mapping profile for <see cref="User"/>.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<SignUpUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LastPasswords, opt => opt.Ignore())
            .ForMember(dest => dest.Roles, opt => opt.Ignore())
            .ForMember(dest => dest.RequestDetails, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom((src, dest, destMember, context) => context.Items[MappingConstants.HashedPassword]));
    }
}