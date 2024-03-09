using AutoMapper;
using CoffeeBlog.Application.MapperProfiles;
using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.ViewModels.Users;
using FluentAssertions;

namespace CoffeeBlog.Application.UnitTests.MapperProfiles;

public class CreateUserViewModelMappingTests
{
    private readonly IMapper _mapper;

    public CreateUserViewModelMappingTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<CreateUserViewModelMapping>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapUserToCreateUserViewModel_whenAdditionalPropertiesAreSpecified()
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
        CreateUserViewModel result = _mapper.Map<CreateUserViewModel>(user, opt => opt.Items[nameof(CreateUserViewModel.JwtToken)] = jwtToken);

        //Assert
        result.UserId.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.Email.Should().Be(user.Email);
        result.JwtToken.Should().Be(jwtToken);
    }

    [Fact]
    public void Map_should_MapUserToCreateUserViewModel_whenAdditionalPropertiesAreNotSpecified()
    {
        //Arrange
        User user = new()
        {
            Id = 1,
            Username = "Johny",
            Email = "myemail@email.com"
        };

        //Act
        Func<CreateUserViewModel> action = () => _mapper.Map<CreateUserViewModel>(user);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}