using AuthService.Application.Commands.Accounts.BanAccountByUserId;
using AuthService.Application.Dtos.Accounts;
using AutoMapper;

namespace AuthService.Application.Mapping.Accounts
{
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
}