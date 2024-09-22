using FluentValidation;
using Shared.Application.AuthService.Validators.SharedValidators;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.AuthService.Commands.Users.SignUpUser;

/// <summary>
/// Validator to validate <see cref="SignUpUserCommand"/>.
/// </summary>
public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommand>
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public SignUpUserCommandValidator()
    {
        RuleFor(x => x.Username).SetValidator(new UsernameValidator());

        //NotEqual(x => x.Username) - Important, because e-mails and usernames are unique in database.
        //Moreover there must not be any username equal to any e-mail due to this program architecture and expectations.
        RuleFor(x => x.Email).SetValidator(new EmailValidator())
                             .NotEqual(x => x.Username)
                             .WithMessage(ValidatorMessages.EmailMustBeDifferentFromUsername);

        RuleFor(x => x.Password).SetValidator(new PasswordValidator());

        RuleFor(x => x.ConfirmPassword).NotEmpty()
                                       .WithMessage(ValidatorMessages.ConfirmPasswordIsRequired)
                                       .Equal(x => x.Password)
                                       .WithMessage(ValidatorMessages.PasswordAndConfirmPasswordMustMatch);
    }
}