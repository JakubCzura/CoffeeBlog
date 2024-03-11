using AutoMapper;
using CoffeeBlog.Application.ExtensionMethods.AutoMapper;
using CoffeeBlog.Application.MappingProfiles;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.ExtensionMethods.AutoMapper;

public class AutoMapperExtensionsTests
{
    private readonly IMapper _mapper;

    public AutoMapperExtensionsTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<CreateUserViewModelMappingProfile>());
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