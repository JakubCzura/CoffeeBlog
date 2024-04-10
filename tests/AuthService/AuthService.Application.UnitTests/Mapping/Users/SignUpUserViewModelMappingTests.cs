using AuthService.Application.Mapping.Users;
using AuthService.Domain.Entities;
using AuthService.Domain.ViewModels.Users;
using AutoMapper;
using FluentAssertions;

namespace AuthService.Application.UnitTests.Mapping.Users;

public class SignUpUserViewModelMappingTests
{
    private readonly IMapper _mapper;

    public SignUpUserViewModelMappingTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<SignUpUserViewModelMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapUserToSignUpUserViewModel_when_AdditionalPropertiesAreSpecified()
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
        SignUpUserViewModel result = _mapper.Map<SignUpUserViewModel>(user, opt => opt.Items[nameof(SignUpUserViewModel.JwtToken)] = jwtToken);

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
        Func<SignUpUserViewModel> action = () => _mapper.Map<SignUpUserViewModel>(user);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}