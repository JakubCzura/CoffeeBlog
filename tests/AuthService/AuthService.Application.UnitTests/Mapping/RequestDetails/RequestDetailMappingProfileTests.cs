using AutoMapper;
using AuthService.Application.Mapping.RequestDetails;
using AuthService.Domain.Commands.RequestDetails;
using AuthService.Domain.Entities;
using FluentAssertions;

namespace AuthService.Application.UnitTests.Mapping.RequestDetails;

public class RequestDetailMappingProfileTests
{
    private readonly IMapper _mapper;

    public RequestDetailMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<RequestDetailMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapCreateRequestDetailCommandToRequestDetail()
    {
        //Arrange
        CreateRequestDetailCommand createRequestDetailCommand = new()
        {
            ControllerName = "UserController",
            Path = "/api/user",
            HttpMethod = "GET",
            StatusCode = 200,
            RequestBody = """{"username":"Johny"}""",
            RequestContentType = "application/json",
            ResponseBody = """{"id":1,"username":"Johny"}""",
            ResponseContentType = "application/json",
            RequestTimeInMiliseconds = 5,
            UserId = 1
        };

        //Act
        RequestDetail result = _mapper.Map<RequestDetail>(createRequestDetailCommand);

        //Assert
        result.ControllerName.Should().Be(createRequestDetailCommand.ControllerName);
        result.Path.Should().Be(createRequestDetailCommand.Path);
        result.HttpMethod.Should().Be(createRequestDetailCommand.HttpMethod);
        result.StatusCode.Should().Be(createRequestDetailCommand.StatusCode);
        result.RequestBody.Should().Be(createRequestDetailCommand.RequestBody);
        result.RequestContentType.Should().Be(createRequestDetailCommand.RequestContentType);
        result.ResponseBody.Should().Be(createRequestDetailCommand.ResponseBody);
        result.ResponseContentType.Should().Be(createRequestDetailCommand.ResponseContentType);
        result.RequestTimeInMiliseconds.Should().Be(createRequestDetailCommand.RequestTimeInMiliseconds);
        result.UserId.Should().Be(createRequestDetailCommand.UserId);
    }
}