﻿using AutoMapper;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;

namespace CoffeeBlog.Application.Mapping.Users;

/// <summary>
/// Mapping profile for <see cref="CreateUserViewModel"/>.
/// </summary>
public class CreateUserViewModelMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public CreateUserViewModelMappingProfile()
    {
        CreateMap<User, CreateUserViewModel>()
            .ForCtorParam(nameof(CreateUserViewModel.UserId), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(CreateUserViewModel.Username), opt => opt.MapFrom(src => src.Username))
            .ForCtorParam(nameof(CreateUserViewModel.Email), opt => opt.MapFrom(src => src.Email))
            .ForCtorParam(nameof(CreateUserViewModel.JwtToken), opt => opt.MapFrom((src, context) => context.Items[nameof(CreateUserViewModel.JwtToken)]));
    }
}