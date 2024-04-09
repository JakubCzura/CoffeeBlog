using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Commands.Users;
using FluentValidation;

namespace AuthService.Application.Validators.CommandsValidators.Users;

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