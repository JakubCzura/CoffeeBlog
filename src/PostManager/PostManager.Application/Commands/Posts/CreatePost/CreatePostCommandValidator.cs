using FluentValidation;
using PostManager.Domain.Resources;

namespace PostManager.Application.Commands.Posts.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
                             .WithMessage(ValidatorMessages.TitleIsRequired)
                             .MaximumLength(100)
                             .WithMessage(ValidatorMessages.TitleCantContainMoreThan100Characters);
    }
}