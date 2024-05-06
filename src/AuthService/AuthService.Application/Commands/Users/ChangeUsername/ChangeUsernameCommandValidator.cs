using AuthService.Application.Validators.SharedValidators;
using FluentValidation;

namespace AuthService.Application.Commands.Users.ChangeUsername;

/// <summary>
/// Validator to validate <see cref="ChangeUsernameCommand"/>.
/// </summary>
public class ChangeUsernameCommandValidator : AbstractValidator<ChangeUsernameCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public ChangeUsernameCommandValidator()
        => RuleFor(x => x.NewUsername).SetValidator(new UsernameValidator());
}