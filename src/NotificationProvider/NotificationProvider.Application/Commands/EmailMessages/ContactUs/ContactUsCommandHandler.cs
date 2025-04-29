using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;
using Shared.Application.NotificationProvider.Commands.EmailMessages.ContactUs;

namespace NotificationProvider.Application.Commands.EmailMessages.ContactUs;

public class ContactUsCommandHandler : IRequestHandler<ContactUsCommand, Result<ResponseBase>>
{
    public Task<Result<ResponseBase>> Handle(ContactUsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}