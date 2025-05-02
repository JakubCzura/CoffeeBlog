using FluentResults;
using MediatR;
using Microsoft.Extensions.Options;
using NotificationProvider.Application.Interfaces.Factories.Emails;
using NotificationProvider.Application.Interfaces.Helpers;
using NotificationProvider.Application.Interfaces.Persistence.Repositories;
using NotificationProvider.Domain.Entities;
using NotificationProvider.Domain.Enums;
using NotificationProvider.Domain.SettingsOptions.Email;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

namespace NotificationProvider.Application.Commands.EmailMessages.ContactUs;

public class ContactUsCommandHandler(IOptions<EmailOptions> _emailOptions,
                                     IEmailMessageRepository emailMessageRepository,
                                     IEmailMessageFactory emailMessageFactory,
                                     IDateTimeProvider dateTimeProvider)
    : IRequestHandler<ContactUsCommand, Result<ResponseBase>>
{
    private readonly EmailOptions _emailOptions = _emailOptions.Value;

    public async Task<Result<ResponseBase>> Handle(ContactUsCommand request, CancellationToken cancellationToken)
    {
        string body = emailMessageFactory.CreateContactUsBody(request.Name, request.Surname, request.Email, request.Message);

        EmailMessage emailMessage = new()
        {
            SenderName = _emailOptions.CoffeeBlog.SenderName,
            SenderEmail = _emailOptions.CoffeeBlog.Email,
            RecipientEmail = _emailOptions.CoffeeBlog.Email,
            Subject = "User submitted contact us form",
            Body = body,
            MessageStatus = EmailMessageStatus.Queued,
            CreatedAt = dateTimeProvider.UtcNow,
            SentAt = dateTimeProvider.UtcNow
        };

        await emailMessageRepository.CreateAsync(emailMessage, cancellationToken);

        return Result.Ok(new ResponseBase());
    }
}