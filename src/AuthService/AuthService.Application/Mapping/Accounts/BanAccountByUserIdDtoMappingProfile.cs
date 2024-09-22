using AuthService.Application.Dtos.Accounts.Repository;
using AutoMapper;
using Shared.Application.AuthService.Commands.Accounts.BanAccountByUserId;

namespace AuthService.Application.Mapping.Accounts;

/// <summary>
/// Mapping profile for <see cref="BanAccountByUserIdDtoMappingProfile"/>.
/// </summary>
public class BanAccountByUserIdDtoMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public BanAccountByUserIdDtoMappingProfile()
    {
        CreateMap<BanAccountByUserIdCommand, BanAccountByUserIdDto>();
    }
}