using AuthService.Application.ExtensionMethods.Automapper.Users;
using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Responses.Users;

namespace AuthService.Application.UnitTests.ExtensionMethods.Automapper.Users;

public class AutoMapperForSignUpUserResponseExtensionsTests
{
    private readonly IMapper _mapper;

    public AutoMapperForSignUpUserResponseExtensionsTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<SignUpUserResponseMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapUserToSignUpUserResponse_when_AdditionalPropertiesAreSpecified()
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
        SignUpUserResponse result = _mapper.Map<SignUpUserResponse>(user, jwtToken);

        //Assert
        result.UserId.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.JwtToken.Should().Be(jwtToken);
    }
}