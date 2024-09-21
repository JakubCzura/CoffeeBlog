using AuthService.Application.Commands.ApiErrors.CreateApiError;
using AuthService.Application.Mapping.ApiErrors;
using AuthService.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Commands.ApiErrors.CreateApiError;

namespace AuthService.Application.UnitTests.Mapping.ApiErrors;

public class ApiErrorMappingProfileTests
{
    private readonly IMapper _mapper;

    public ApiErrorMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<ApiErrorMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapCreateApiErrorCommanddToApiError()
    {
        //Arrange
        CreateApiErrorCommand createApiErrorCommand = new(
            "NullEntityException",
            "NullEntityException, StackTrace: example for test, More auto-generated text etc.",
            "Entity cannot be null",
            "Attempt to save null entity in database"
        );

        //Act
        ApiError result = _mapper.Map<ApiError>(createApiErrorCommand);

        //Assert
        result.Name.Should().Be(createApiErrorCommand.Name);
        result.Exception.Should().Be(createApiErrorCommand.Exception);
        result.Message.Should().Be(createApiErrorCommand.Message);
        result.Description.Should().Be(createApiErrorCommand.Description);
    }
}