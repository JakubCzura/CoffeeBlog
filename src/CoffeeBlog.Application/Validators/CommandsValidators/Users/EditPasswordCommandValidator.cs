using CoffeeBlog.Application.Validators.SharedValidators;
using CoffeeBlog.Domain.Commands.Users;
using CoffeeBlog.Domain.Resources;
using FluentValidation;

namespace CoffeeBlog.Application.Validators.CommandsValidators.Users;

/// <summary>
/// Validator to validate <see cref="EditPasswordCommand"/>.
/// </summary>
public class EditPasswordCommandValidator : AbstractValidator<EditPasswordCommand>
{
    ///<summary>
    /// Default constructor.
    /// </summary>
    public EditPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                          .Equal(x => x.NewPassword).WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}