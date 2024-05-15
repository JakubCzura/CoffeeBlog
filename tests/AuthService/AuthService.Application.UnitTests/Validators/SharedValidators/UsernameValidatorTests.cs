using AuthService.Application.Validators.SharedValidators;
using AuthService.Domain.Resources;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace AuthService.Application.UnitTests.Validators.SharedValidators;

public class UsernameValidatorTests
{
    private readonly UsernameValidator _usernameValidator = new();

    public static TheoryData<string> Validate_should_Pass_when_UsernameIsCorrect_Data => new()
    {
        "Johny",
        "J",
        new string('K', 50)
    };

    [Theory]
    [MemberData(nameof(Validate_should_Pass_when_UsernameIsCorrect_Data))]
    public void Validate_should_Pass_when_UsernameIsCorrect(string username)
        => _usernameValidator.TestValidate(username)
                             .ShouldNotHaveAnyValidationErrors();

    public static TheoryData<string, string> Validate_should_Fail_when_UsernameIsIncorrect_Data => new()
    {
        { "", ValidatorMessages.UsernameIsRequired } ,
        { " ", ValidatorMessages.UsernameIsRequired } ,
        { "   ", ValidatorMessages.UsernameIsRequired } ,
        { new string('k', 51), ValidatorMessages.UsernameCantContainMoreThan50Characters }
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_UsernameIsIncorrect_Data))]
    public void Validate_should_Fail_when_UsernameIsIncorrect(string username,
                                                              string errorMessage)
        => _usernameValidator.TestValidate(username)
                             .ShouldHaveAnyValidationError()
                             .WithErrorMessage(errorMessage);

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_UsernameIsNull()
        => _usernameValidator.Invoking(x => x.TestValidate(null!))
                             .Should()
                             .Throw<ArgumentNullException>();
}