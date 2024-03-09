using AutoMapper;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;

namespace CoffeeBlog.Application.MapperProfiles;

public class CreateUserViewModelMapping : Profile
{
    public CreateUserViewModelMapping()
    {
        CreateMap<User, CreateUserViewModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.JwtToken, opt => opt.MapFrom((src, dest, destMember, context) => context.Items[nameof(CreateUserViewModel.JwtToken)]));
    }
}