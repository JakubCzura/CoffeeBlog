using CoffeeBlog.Application.Validators.SharedValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.SharedValidators;

public class PasswordValidatorTests
{
    private readonly PasswordValidator _passwordValidator = new();

    public static IEnumerable<object[]> Validate_should_Pass_when_PasswordIsCorrect_Data()
    {
        yield return new object[] { "Jo@2d" };
        yield return new object[] { "ok2!!!!D" };
        yield return new object[] { "St@4Tp" };
    }

    [Theory]
    [MemberData(nameof(Validate_should_Pass_when_PasswordIsCorrect_Data))]
    public void Validate_should_Pass_when_PasswordIsCorrect(string password)
        => _passwordValidator.TestValidate(password)
                             .ShouldNotHaveAnyValidationErrors();

    public static IEnumerable<object[]> Validate_should_Fail_when_PasswordIsIncorrect_Data()
    {
        yield return new object[] { "" };
        yield return new object[] { " " };
        yield return new object[] { "  " };
        yield return new object[] { "s" };
        yield return new object[] { " d" };
        yield return new object[] { "d s" };
        yield return new object[] { "4" };
        yield return new object[] { "1234" };
        yield return new object[] { "1234" };
        yield return new object[] { "12345" };
        yield return new object[] { "Johny1" };
        yield return new object[] { "!y1" };
        yield return new object[] { "!y1    " };
        yield return new object[] { "!#" };
        yield return new object[] { "k^" };
        yield return new object[] { "K^ 1" };
        yield return new object[] { new string('k', 321) };
    }

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