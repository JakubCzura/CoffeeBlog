using FluentValidation;
using Shared.Application.AuthService.Constants.Policy;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.AuthService.Validators.SharedValidators;

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
                                        .MaximumLength(UsernamePolicyConstants.MaxLength)
                                        .WithMessage(string.Format(ValidatorMessages.UsernameCantContainMoreThan_0_Characters, UsernamePolicyConstants.MaxLength));
}