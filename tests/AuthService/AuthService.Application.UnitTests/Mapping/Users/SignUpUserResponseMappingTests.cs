using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Responses.Users;

namespace AuthService.Application.UnitTests.Mapping.Users;

public class SignUpUserResponseMappingTests
{
    private readonly IMapper _mapper;

    public SignUpUserResponseMappingTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<SignUpUserResponseMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapUserToSignUpUserResponse_when_AdditionalPropertiesAreSpecified()
    {
        //Arrange
        string jwtToken = "jwtToken";

        User user = new()
        {
            Id = 1,
            Username = "Johny",
            Email = "myemail@email.com"
        };

        //Act
        SignUpUserResponse result = _mapper.Map<SignUpUserResponse>(user, opt => opt.Items[nameof(SignUpUserResponse.JwtToken)] = jwtToken);

        //Assert
        result.UserId.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.JwtToken.Should().Be(jwtToken);
    }

    [Fact]
    public void Map_should_ThrowAutoMapperMappingException_when_AdditionalPropertiesAreNotSpecified()
    {
        //Arrange
        User user = new()
        {
            Id = 1,
            Username = "Johny",
            Email = "myemail@email.com"
        };

        //Act
        Func<SignUpUserResponse> action = () => _mapper.Map<SignUpUserResponse>(user);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}