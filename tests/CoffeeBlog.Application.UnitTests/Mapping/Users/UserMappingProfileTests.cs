using AutoMapper;
using CoffeeBlog.Application.Mapping.Users;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.Mapping.Users;

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
    public void Map_should_MapCreateUserCommandToUser_when_AdditionalPropertiesAreSpecified()
    {
        //Arrange
        string hashedPassword = "#$#@sdasdjsahd218732DSADASD@#dksadha23!@#das";
        DateTime createdAt = DateTime.UtcNow;

        CreateUserCommand createUserCommand = new()
        {
            Username = "Johny",
            Email = "johny@email.com",
            Password = "Password123@!",
            ConfirmPassword = "Password123@!"
        };

        //Act
        User result = _mapper.Map<User>(createUserCommand, opt =>
        {
            opt.Items[MappingConstants.HashedPassword] = hashedPassword;
            opt.Items[MappingConstants.CreatedAt] = createdAt;
        });

        //Assert
        result.Username.Should().Be(createUserCommand.Username);
        result.Email.Should().Be(createUserCommand.Email);
        result.Password.Should().Be(hashedPassword);
        result.CreatedAt.Should().Be(createdAt);
    }

    [Fact]
    public void Map_should_ThrowAutoMapperMappingException_when_AdditionalPropertiesAreNotSpecified()
    {
        //Arrange
        CreateUserCommand createUserCommand = new()
        {
            Username = "Johny",
            Email = "johny@email.com"
        };

        //Act
        Func<User> action = () => _mapper.Map<User>(createUserCommand);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}