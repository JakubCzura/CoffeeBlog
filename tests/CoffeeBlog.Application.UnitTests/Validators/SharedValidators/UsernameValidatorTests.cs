using CoffeeBlog.Application.Validators.SharedValidators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CoffeeBlog.Application.UnitTests.Validators.SharedValidators;

public class UsernameValidatorTests
{
    private readonly UsernameValidator _usernameValidator = new();

    public static IEnumerable<object[]> Validate_should_Pass_when_UsernameIsCorrect_Data()
    {
        yield return new object[] { "Johny" };
        yield return new object[] { "J" };
        yield return new object[] { new string('K', 100) };
    }

    [Theory]
    [MemberData(nameof(Validate_should_Pass_when_UsernameIsCorrect_Data))]
    public void Validate_should_Pass_when_UsernameIsCorrect(string username)
        => _usernameValidator.TestValidate(username)
                             .ShouldNotHaveAnyValidationErrors();

    public static IEnumerable<object[]> Validate_should_Fail_when_UsernameIsIncorrect_Data()
    {
        yield return new object[] { "" };
        yield return new object[] { new string('k', 101) };
    }

    [Theory]
    [MemberData(nameof(Validate_should_Fail_when_UsernameIsIncorrect_Data))]
    public void Validate_should_Fail_when_UsernameIsIncorrect(string username)
        => _usernameValidator.TestValidate(username)
                             .ShouldHaveAnyValidationError();

    [Fact]
    public void Validate_should_ThrowArgumentNullException_when_UsernameIsNull()
        => _usernameValidator.Invoking(x => x.TestValidate(null!))
                             .Should()
                             .Throw<ArgumentNullException>();
}