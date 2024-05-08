using AuthService.Application.Commands.Accounts.BanAccountByUserId;
using AuthService.Application.Dtos.Accounts;
using AuthService.Application.Mapping.Accounts;
using AuthService.Domain.Enums;
using AutoMapper;
using FluentAssertions;

namespace AuthService.Application.UnitTests.Mapping.Accounts;

public class BanAccountByUserIdDtoMappingProfileTests
{
    private readonly IMapper _mapper;

    public BanAccountByUserIdDtoMappingProfileTests()
    {
        MapperConfiguration configurationProvider = new(cfg => cfg.AddProfile<BanAccountByUserIdDtoMappingProfile>());
        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapper_should_HaveValidConfiguration()
        => _mapper.ConfigurationProvider.AssertConfigurationIsValid();

    [Fact]
    public void Map_should_MapBanAccountByUserIdCommandToBanAccountByUserIdDto()
    {
        //Arrange
        BanAccountByUserIdCommand banAccountByUserIdCommand = new(1, AccountBanReason.Spamming, "Spamming too much", DateTime.UtcNow);

        //Act
        BanAccountByUserIdDto result = _mapper.Map<BanAccountByUserIdDto>(banAccountByUserIdCommand);

        //Assert
        result.UserId.Should().Be(banAccountByUserIdCommand.UserId);
        result.BanReason.Should().Be(banAccountByUserIdCommand.BanReason);
        result.BanNote.Should().Be(banAccountByUserIdCommand.BanNote);
        result.BanEndsAt.Should().Be(banAccountByUserIdCommand.BanEndsAt);
    }
}