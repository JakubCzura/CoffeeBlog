using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.ConfirmNewsletterSubscription;

public class ConfirmNewsletterSubscriptionCommand : IRequest<Result<ResponseBase>>
{
    public string Id { get; set; } = string.Empty;
}