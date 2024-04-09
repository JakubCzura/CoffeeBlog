using AuthService.Application.Validators.SharedValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

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

    public static TheoryData<string> Validate_should_Fail_when_PasswordIsIncorrect_Data => new()
    {
        "",
        " ",
        "  ",
        "s" ,
        " d" ,
        "d s" ,
        "4" ,
        "1234" ,
        "1234" ,
        "12345",
        "Johny1" ,
        "!y1" ,
        "!y1    " ,
        "!#" ,
        "k^" ,
        "K^ 1" ,
        new string('k', 321)
    };

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_PasswordIsIncorrect_Data))]
    public void Validate_should_Fail_when_PasswordIsIncorrect(string password)
        => _passwordValidator.TestValidate(password)
                             .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_PasswordIsNull()
        => _passwordValidator.Invoking(x => x.TestValidate(null!))
                             .Should()
                             .Throw<ArgumentNullException>();
}