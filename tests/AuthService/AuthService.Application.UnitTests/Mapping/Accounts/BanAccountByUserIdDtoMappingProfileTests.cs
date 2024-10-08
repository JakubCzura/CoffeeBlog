﻿using AuthService.Application.Dtos.Accounts.Repository;
using AuthService.Application.Mapping.Accounts;
using AutoMapper;
using FluentAssertions;
using Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;
using Shared.Domain.AuthService.Enums;

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
        BanAccountByUserIdCommand banAccountByUserIdCommand = new(1, AccountBanReason.Spamming, "Spamming too much", new DateOnly(2030, 1, 1));

        //Act
        BanAccountByUserIdDto result = _mapper.Map<BanAccountByUserIdDto>(banAccountByUserIdCommand);

        //Assert
        result.UserId.Should().Be(banAccountByUserIdCommand.UserId);
        result.BanReason.Should().Be(banAccountByUserIdCommand.BanReason);
        result.BanNote.Should().Be(banAccountByUserIdCommand.BanNote);
        result.BanEndsAt.Should().Be(banAccountByUserIdCommand.BanEndsAt);
    }
}