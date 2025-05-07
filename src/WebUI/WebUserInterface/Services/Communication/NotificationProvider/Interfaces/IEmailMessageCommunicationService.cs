using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

namespace WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

public interface IEmailMessageCommunicationService
{
    Task<ResponseBase> ContactUsAsync(ContactUsCommand contactUsCommand,
                                      CancellationToken cancellationToken);
}