using FluentAssertions;
using FluentValidation.TestHelper;
using Shared.Application.AuthService.Constants.Policy;
using Shared.Application.AuthService.Validators.SharedValidators;
using Shared.Domain.Common.Resources.Translations;

namespace Shared.Application.UnitTests.AuthService.Validators.SharedValidators;

public class PasswordValidatorTests
{
    private readonly PasswordValidator _passwordValidator = new();

    public static TheoryData<string> Validate_should_Pass_when_PasswordIsCorrect_Data => new()
    {
       "Jo@2d",
        "ok2!!!!D" ,
        "St@4Tp" ,
        new string('k', 45) + "$1D3@"
    };

    [Theory]
    [MemberData(nameof(Validate_should_Pass_when_PasswordIsCorrect_Data))]
    public void Validate_should_Pass_when_PasswordIsCorrect(string password)
        => _passwordValidator.TestValidate(password)
                             .ShouldNotHaveAnyValidationErrors();

    public static TheoryData<string, string> Validate_should_Fail_when_PasswordIsIncorrect_Data => new()
    {
        { "", ValidatorMessages.PasswordIsRequired },
        { " ", ValidatorMessages.PasswordIsRequired },
        { "  ", ValidatorMessages.PasswordIsRequired },
        { "@#", string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength) },
        { " d@1", string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength) },
        { "d#!", string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength) },
        { new string('k', 4), string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength) },
        { new string('k', 51), string.Format(ValidatorMessages.PasswordMustBeBetween_0_And_1_CharactersLong, PasswordPolicyConstants.MinLength, PasswordPolicyConstants.MaxLength) },
        { "d12#$da23", ValidatorMessages.PasswordMustContainAtLeastOneUpperLetter },
        { "3DDD23$$!@31@O", ValidatorMessages.PasswordMustContainAtLeastOneLowerLetter },
        { "DDD@@@ddad@@$#", ValidatorMessages.PasswordMustContainAtLeastOneDigit },
        { "kdD2312dsa32d", string.Format(ValidatorMessages.PasswordMustContainAtLeastOneOfSpecialCharacters__0_, PasswordPolicyConstants.SpecialCharacters) }
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_PasswordIsIncorrect_Data))]
    public void Validate_should_Fail_when_PasswordIsIncorrect(string password,
                                                              string errorMessage)
        => _passwordValidator.TestValidate(password)
                             .ShouldHaveAnyValidationError()
                             .WithErrorMessage(errorMessage);

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_PasswordIsNull()
        => _passwordValidator.Invoking(x => x.TestValidate(null!))
                             .Should()
                             .Throw<ArgumentNullException>();
}