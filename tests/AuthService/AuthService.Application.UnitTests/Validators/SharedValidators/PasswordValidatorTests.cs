using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;
using Shared.Application.AuthService.Constants;
using Shared.Application.AuthService.Validators.SharedValidators;

namespace AuthService.Application.UnitTests.Validators.SharedValidators;

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
        { "@#", ValidatorMessages.PasswordMustBeBetween5And50CharactersLong },
        { " d@1", ValidatorMessages.PasswordMustBeBetween5And50CharactersLong },
        { "d#!", ValidatorMessages.PasswordMustBeBetween5And50CharactersLong },
        { new string('k', 4), ValidatorMessages.PasswordMustBeBetween5And50CharactersLong },
        { new string('k', 51), ValidatorMessages.PasswordMustBeBetween5And50CharactersLong },
        { "d12#$da23", ValidatorMessages.PasswordMustContainAtLeastOneUpperLetter },
        { "3DDD23$$!@31@O", ValidatorMessages.PasswordMustContainAtLeastOneLowerLetter },
        { "DDD@@@ddad@@$#", ValidatorMessages.PasswordMustContainAtLeastOneDigit },
        { "kdD2312dsa32d", $"{ValidatorMessages.PasswordMustContainAtLeastOneOfSpecialCharacters}: {PasswordConstants.SpecialCharacters}" }
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