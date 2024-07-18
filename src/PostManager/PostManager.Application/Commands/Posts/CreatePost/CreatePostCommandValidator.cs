using FluentValidation;
using PostManager.Domain.Resources;

namespace PostManager.Application.Commands.Posts.CreatePost;

/// <summary>
/// Validator to validate <see cref="CreatePostCommand"/>.
/// </summary>
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                             .WithMessage(ValidatorMessages.TitleIsRequired)
                             .MaximumLength(100)
                             .WithMessage(ValidatorMessages.TitleCantContainMoreThan100Characters);

        RuleFor(x => x.Subtitle).MaximumLength(100)
                                .WithMessage(ValidatorMessages.SubtitleCantContainMoreThan100Characters);

        RuleFor(x => x.Content).NotEmpty()
                               .WithMessage(ValidatorMessages.ContentIsRequired)
                               .MaximumLength(5000)
                               .WithMessage(ValidatorMessages.ContentCantContainMoreThan5000Characters);
    }
}