using AutoMapper;
using CoffeeBlog.Application.ExtensionMethods.Automapper.Users;
using CoffeeBlog.Application.Mapping.Users;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Entities;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.ExtensionMethods.Automapper.Users;

public class AutoMapperForUserExtensions
{
    private readonly IMapper _mapper;

    public AutoMapperForUserExtensions()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<UserMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapCreateUserCommandToUser_when_AdditionalPropertiesAreSpecified()
    {
        // Arrange
        string hashedPassword = "hjsda@#!@dasdn2#!@da@#!";

        CreateUserCommand createUserCommand = new()
        {
            Username = "username",
            Email = "myemail@email.com"
        };

        //Act
        User result = _mapper.Map<User>(createUserCommand, hashedPassword);

        //Assert
        result.Username.Should().Be(createUserCommand.Username);
        result.Email.Should().Be(createUserCommand.Email);
        result.Password.Should().Be(hashedPassword);
    }
}