using FluentValidation;
using Shared.Domain.AuthService.Resources;

namespace AuthService.Application.Validators.SharedValidators;

/// <summary>
/// Validator to validate user's username.
/// </summary>
public class UsernameValidator : AbstractValidator<string>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public UsernameValidator()
        => RuleFor(username => username).NotEmpty()
                                        .WithMessage(ValidatorMessages.UsernameIsRequired)
                                        .MaximumLength(50)
                                        .WithMessage(ValidatorMessages.UsernameCantContainMoreThan50Characters);
}