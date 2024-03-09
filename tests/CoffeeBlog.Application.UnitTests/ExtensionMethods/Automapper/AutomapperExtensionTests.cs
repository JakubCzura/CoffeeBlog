using AutoMapper;
using CoffeeBlog.Application.MapperProfiles;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using CoffeeBlog.Application.ExtensionMethods.Automapper;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.ExtensionMethods.Automapper;

public class AutomapperExtensionTests
{
    private readonly IMapper _mapper;

    public AutomapperExtensionTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<CreateUserViewModelMapping>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapUserWithAdditionalPropertiesToCreateUserViewModel_when_AdditionalPropertiesAreSpecified()
    {
        // Arrange
        string jwtToken = "jwtToken";

        User user = new()
        {
            Id = 1,
            Username = "username",
            Email = "myemail@email.com"
        };

        //Act
        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(user, jwtToken);

        //Assert
        result.UserId.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.JwtToken.Should().Be(jwtToken);
    }
}