using AutoMapper;
using EventBus.Domain.Responses.AuthService.UserDiagnostics;
using FluentAssertions;
using StatisticsCollector.Application.Mapping.UsersDiagnostics;

namespace StatisticsCollector.Application.UnitTests.Mapping.UsersDiagnostics;

public class UsersDiagnosticsMappingProfileTests
{
    private readonly IMapper _mapper;

    public UsersDiagnosticsMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<UsersDiagnosticsMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapGetUsersDiagnosticDataResponseToUsersDiagnostics()
    {
        //Arrange
        GetUsersDiagnosticDataResponse getUsersDiagnosticDataResponse = new(
            2,
            10,
            3,
            "Spamming",
            5,
            2,
            1,
            3,
            3,
            DateTime.UtcNow,
            Guid.NewGuid(),
            "RequestConsumerName");

        //Act
        Domain.Entities.UsersDiagnostics result = _mapper.Map<Domain.Entities.UsersDiagnostics>(getUsersDiagnosticDataResponse);

        //Assert
        result.NewUserCount.Should().Be(getUsersDiagnosticDataResponse.NewUserCount);
        result.ActiveAccountCount.Should().Be(getUsersDiagnosticDataResponse.ActiveAccountCount);
        result.BannedAccountCount.Should().Be(getUsersDiagnosticDataResponse.BannedAccountCount);
        result.MostCommonBanReason.Should().Be(getUsersDiagnosticDataResponse.MostCommonBanReason);
        result.UserWhoLoggedInCount.Should().Be(getUsersDiagnosticDataResponse.UserWhoLoggedInCount);
        result.UserWhoFailedToLogInCount.Should().Be(getUsersDiagnosticDataResponse.UserWhoFailedToLogInCount);
        result.UserWhoChangedUsernameCount.Should().Be(getUsersDiagnosticDataResponse.UserWhoChangedUsernameCount);
        result.UserWhoChangedEmailCount.Should().Be(getUsersDiagnosticDataResponse.UserWhoChangedEmailCount);
        result.UserWhoChangedPasswordCount.Should().Be(getUsersDiagnosticDataResponse.UserWhoChangedPasswordCount);
        result.DataCollectedAt.Should().Be(getUsersDiagnosticDataResponse.DataCollectedAt);
    }
}