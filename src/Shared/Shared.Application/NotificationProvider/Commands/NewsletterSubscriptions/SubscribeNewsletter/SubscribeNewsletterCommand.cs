using FluentResults;
using MediatR;
using Shared.Application.Common.Responses.Basics;

namespace Shared.Application.NotificationProvider.Commands.NewsletterSubscriptions.SubscribeNewsletter;

public class SubscribeNewsletterCommand : IRequest<Result<ResponseBase>>
{
    public string Email { get; set; } = string.Empty;
    public bool AgreeToTerms { get; set; }
}