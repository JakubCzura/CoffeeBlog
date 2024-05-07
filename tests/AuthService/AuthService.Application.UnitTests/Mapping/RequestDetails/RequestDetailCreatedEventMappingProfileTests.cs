using AuthService.Application.Commands.RequestDetails.CreateRequestDetail;
using AuthService.Application.Mapping.RequestDetails;
using AuthService.Domain.Constants;
using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using FluentAssertions;

namespace AuthService.Application.UnitTests.Mapping.RequestDetails;

public class RequestDetailCreatedEventMappingProfileTests
{
    private readonly IMapper _mapper;

    public RequestDetailCreatedEventMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<RequestDetailCreatedEventMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapCreateRequestDetailCommandToRequestDetailCreatedEvent()
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
        RequestDetailCreatedEvent result = _mapper.Map<RequestDetailCreatedEvent>(createRequestDetailCommand, opt =>
        {
            opt.Items[nameof(RequestDetailCreatedEvent.EventPublisherName)] = eventPublisherName;
        });

        //Assert
        result.MicroserviceName.Should().Be(MicroserviceInfoConstants.Name);
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

    [Fact]
    public void Map_should_ThrowAutoMapperMappingException_when_AdditionalPropertiesAreNotSpecified()
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
            UserId = 1,
        };

        //Act
        Func<RequestDetailCreatedEvent> action = () => _mapper.Map<RequestDetailCreatedEvent>(createRequestDetailCommand);

        //Assert
        action.Should().Throw<AutoMapperMappingException>();
    }
}