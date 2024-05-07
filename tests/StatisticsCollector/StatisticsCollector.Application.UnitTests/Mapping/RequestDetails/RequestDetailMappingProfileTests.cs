using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using FluentAssertions;
using StatisticsCollector.Application.Mapping.RequetsDetails;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Application.UnitTests.Mapping.RequestDetails;

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
    public void Map_should_MapRequestDetailCreatedEventToRequestDetail()
    {
        //Arrange
        RequestDetailCreatedEvent requestDetailCreatedEvent = new(
            "UserController",
            "/api/user",
            "GET",
            200,
            """{"username":"Johny"}""",
            "application/json",
            """{"id":1,"username":"Johny"}""",
            "application/json",
            5,
            DateTime.UtcNow,
            1,
            "RequestCommandHandler",
            "MicroserviceName");

        //Act
        RequestDetail result = _mapper.Map<RequestDetail>(requestDetailCreatedEvent);

        //Assert
        result.ControllerName.Should().Be(requestDetailCreatedEvent.ControllerName);
        result.Path.Should().Be(requestDetailCreatedEvent.Path);
        result.HttpMethod.Should().Be(requestDetailCreatedEvent.HttpMethod);
        result.StatusCode.Should().Be(requestDetailCreatedEvent.StatusCode);
        result.RequestBody.Should().Be(requestDetailCreatedEvent.RequestBody);
        result.RequestContentType.Should().Be(requestDetailCreatedEvent.RequestContentType);
        result.ResponseBody.Should().Be(requestDetailCreatedEvent.ResponseBody);
        result.ResponseContentType.Should().Be(requestDetailCreatedEvent.ResponseContentType);
        result.RequestTimeInMiliseconds.Should().Be(requestDetailCreatedEvent.RequestTimeInMiliseconds);
        result.SentAt.Should().Be(requestDetailCreatedEvent.SentAt);
        result.UserId.Should().Be(requestDetailCreatedEvent.UserId);
        result.MicroserviceName.Should().Be(requestDetailCreatedEvent.EventPublisherMicroserviceName);
    }
}