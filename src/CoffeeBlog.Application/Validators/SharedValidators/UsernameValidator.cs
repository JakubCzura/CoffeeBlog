using CoffeeBlog.Domain.Resources;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's username.
/// Should be used to validate username when creating new user or updating existing one.
/// </summary>
public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
        => RuleFor(username => username).NotEmpty().WithMessage(ValidatorMessages.UsernameIsRequired)
                                        .MaximumLength(100).WithMessage(ValidatorMessages.UsernameCantContainMoreThan100Characters);
}