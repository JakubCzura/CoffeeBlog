using AutoMapper;
using AuthService.Application.ExtensionMethods.Automapper.Users;
using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AuthService.Domain.ViewModels.Users;
using FluentAssertions;

namespace AuthService.Application.UnitTests.ExtensionMethods.Automapper.Users;

public class AutoMapperForSignUpUserViewModelExtensionsTests
{
    private readonly IMapper _mapper;

    public AutoMapperForSignUpUserViewModelExtensionsTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<SignUpUserViewModelMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapUserToSignUpUserViewModel_when_AdditionalPropertiesAreSpecified()
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
        SignUpUserViewModel result = _mapper.Map<SignUpUserViewModel>(user, jwtToken);

        //Assert
        result.UserId.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.JwtToken.Should().Be(jwtToken);
    }
}