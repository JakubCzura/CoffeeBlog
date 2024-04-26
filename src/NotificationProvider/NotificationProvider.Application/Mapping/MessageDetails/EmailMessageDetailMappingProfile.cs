using AutoMapper;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Mapping.MessageDetails;

/// <summary>
/// Mapping profile for <see cref="EmailMessageDetail"/>.
/// </summary>
public class EmailMessageDetailMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public EmailMessageDetailMappingProfile()
    {
        CreateMap<IEmailMessage, EmailMessageDetail>()
            .ForMember(dest => dest.SentAt, opt => opt.Ignore());
    }
}