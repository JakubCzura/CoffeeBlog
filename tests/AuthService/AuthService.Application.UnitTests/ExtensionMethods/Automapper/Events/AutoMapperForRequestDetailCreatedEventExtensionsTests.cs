using AuthService.Application.Commands.RequestDetails.CreateRequestDetail;
using AuthService.Application.ExtensionMethods.Automapper.Events;
using AuthService.Application.Mapping.RequestDetails;
using AutoMapper;
using EventBus.Domain.Events.AuthService.RequestDetails;
using FluentAssertions;

namespace AuthService.Application.UnitTests.ExtensionMethods.Automapper.Events;

public class AutoMapperForRequestDetailCreatedEventExtensionsTests
{
    private readonly IMapper _mapper;

    public AutoMapperForRequestDetailCreatedEventExtensionsTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<RequestDetailCreatedEventMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void Map_should_MapCreateRequestDetailCommandToRequestDetailCreatedEvent_when_AdditionalPropertiesAreSpecified()
    {
        //Arrange
        string eventPublisherName = "RequestCommandHandler";

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
            UserId = 1,
        };

        //Act
        RequestDetailCreatedEvent result = _mapper.Map<RequestDetailCreatedEvent>(createRequestDetailCommand, eventPublisherName);

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
        result.SentAt.Should().Be(createRequestDetailCommand.SentAt);
        result.UserId.Should().Be(createRequestDetailCommand.UserId);
        result.EventPublisherName.Should().Be(eventPublisherName);
    }
}