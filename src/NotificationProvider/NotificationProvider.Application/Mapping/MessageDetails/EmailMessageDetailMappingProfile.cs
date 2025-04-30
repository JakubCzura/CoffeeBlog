using AutoMapper;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Models.Emails;

namespace NotificationProvider.Application.Mapping.MessageDetails;

/// <summary>
/// Mapping profile for <see cref="EmailMessage"/>.
/// </summary>
public class EmailMessageDetailMappingProfile : Profile
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public EmailMessageDetailMappingProfile()
    {
        CreateMap<IEmailMessage, EmailMessage>()
            .ForMember(dest => dest.SentAt, opt => opt.Ignore());
    }
}