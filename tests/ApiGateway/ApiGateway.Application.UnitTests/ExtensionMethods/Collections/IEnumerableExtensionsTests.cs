using ApiGateway.Application.ExtensionMethods.Collections;
using FluentAssertions;

namespace ApiGateway.Application.UnitTests.ExtensionMethods.Collections;

public class IEnumerableExtensionsTests
{
    public static TheoryData<string?[]> IsAnyElementNullOrWhiteSpace_should_ReturnTrue_when_AnyElementIsNullOrWhiteSpace_Data =>
    [
        ["", "b", "c", "d", "e"],
        ["a", "b", "c", " ", "e"],
        ["a", "b", "c", null, "e"]
    ];

    [Theory]
    [MemberData(nameof(IsAnyElementNullOrWhiteSpace_should_ReturnTrue_when_AnyElementIsNullOrWhiteSpace_Data))]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnTrue_when_AnyElementIsNullOrWhiteSpace(string?[] collection)
        => collection.IsAnyElementNullOrWhiteSpace()
                     .Should()
                     .BeTrue();

    public static TheoryData<string?[]> IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_NoElementIsNullOrWhiteSpace_Data =>
    [
        ["a", "b", "c", "d", "e"],
    ];

    [Theory]
    [MemberData(nameof(IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_NoElementIsNullOrWhiteSpace_Data))]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_NoElementIsNullOrWhiteSpace(string?[] collection)
        => collection.IsAnyElementNullOrWhiteSpace()
                     .Should()
                     .BeFalse();

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_CollectionIsEmpty()
        => Array.Empty<string>().IsAnyElementNullOrWhiteSpace()
                                .Should()
                                .BeFalse();

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ThrowArgumentNullException_when_CollectionIsNull()
    {
        // Arrange
        IEnumerable<string> collection = null!;

        // Act
        Action action = () => collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}