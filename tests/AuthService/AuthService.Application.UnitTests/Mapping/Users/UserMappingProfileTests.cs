using AuthService.Application.Commands.Users.SignUpUser;
using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Commands.Users.SignUpUser;

namespace AuthService.Application.UnitTests.Mapping.Users;

public class UserMappingProfileTests
{
    private readonly IMapper _mapper;

    public UserMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<UserMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapSignUpUserCommandToUser()
    {
        //Arrange
        SignUpUserCommand signUpUserCommand = new(
            "Johny",
            "johny@email.com",
            "Password123@!",
            "Password123@!"
        );

        //Act
        User result = _mapper.Map<User>(signUpUserCommand);

        //Assert
        result.UserName.Should().Be(signUpUserCommand.Username);
        result.Email.Should().Be(signUpUserCommand.Email);
    }
}