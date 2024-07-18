using AuthService.Application.Commands.Users.SignUpUser;
using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;

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
    public void Map_should_MapSignUpUserCommandToUser_when_AdditionalPropertiesAreSpecified()
    {
        //Arrange
        string hashedPassword = "#$#@sdasdjsahd218732DSADASD@#dksadha23!@#das";

        SignUpUserCommand signUpUserCommand = new(
            "Johny",
            "johny@email.com",
            "Password123@!",
            "Password123@!"
        );

        //Act
        User result = _mapper.Map<User>(signUpUserCommand, opt =>
        {
            opt.Items[nameof(User.Password)] = hashedPassword;
        });

        //Assert
        result.Username.Should().Be(signUpUserCommand.Username);
        result.Email.Should().Be(signUpUserCommand.Email);
        result.Password.Should().Be(hashedPassword);
    }

    [Fact]
    public void Map_should_ThrowAutoMapperMappingException_when_AdditionalPropertiesAreNotSpecified()
    {
        //Arrange
        SignUpUserCommand signUpUserCommand = new(
            "Johny",
            "johny@email.com",
            "Password123@!",
            "Password123@!"
        );

        //Act
        Func<User> action = () => _mapper.Map<User>(signUpUserCommand);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}