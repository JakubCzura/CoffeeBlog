using CoffeeBlog.Domain.Resources;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's username.
/// </summary>
public class UsernameValidator : AbstractValidator<string>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public UsernameValidator()
        => RuleFor(username => username).NotEmpty().WithMessage(ValidatorMessages.UsernameIsRequired)
                                        .MaximumLength(100).WithMessage(ValidatorMessages.UsernameCantContainMoreThan100Characters);
}