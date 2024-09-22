using AuthService.Application.ExtensionMethods.Automapper.Users;
using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Commands.Users.SignUpUser;

namespace AuthService.Application.UnitTests.ExtensionMethods.Automapper.Users;

public class AutoMapperForUserExtensions
{
    private readonly IMapper _mapper;

    public AutoMapperForUserExtensions()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<UserMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapSignUpUserCommandToUser_when_AdditionalPropertiesAreSpecified()
    {
        // Arrange
        string hashedPassword = "hjsda@#!@dasdn2#!@da@#!";

        SignUpUserCommand signUpUserCommand = new(
            "username",
            "myemail@email.com",
            "Password123@!",
            "Password123@!"
        );

        //Act
        User result = _mapper.Map<User>(signUpUserCommand, hashedPassword);

        //Assert
        result.Username.Should().Be(signUpUserCommand.Username);
        result.Email.Should().Be(signUpUserCommand.Email);
        result.Password.Should().Be(hashedPassword);
    }
}