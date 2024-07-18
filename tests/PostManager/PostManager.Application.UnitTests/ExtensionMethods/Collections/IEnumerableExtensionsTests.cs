using FluentAssertions;
using FluentResults;
using PostManager.Application.ExtensionMethods.Collections;

namespace PostManager.Application.UnitTests.ExtensionMethods.Collections;

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

    public static TheoryData<IError[], char, string> GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecified_Data => new()
    {
        { new IError[] { new Error("Error 1 Message"), new Error("Error 2 Message") }, ';', "Error 1 Message;Error 2 Message" },
        { new IError[] { new Error("Error 1 Message"), new Error("Error 2 Message") }, ',', "Error 1 Message,Error 2 Message" },
        { new IError[] { new Error("Error 1"), new Error("Error 2") }, ' ', "Error 1 Error 2" }
    };

    [Theory]
    [MemberData(nameof(GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecified_Data))]
    public void GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecified(IError[] errors, char delimiter, string expected)
        => errors.GetJoinedMessages(delimiter)
                .Should()
                .Be(expected);

    [Fact]
    public void GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStringsWithDefaultDelimiter_when_DelimiterIsNotSpecified()
        => new IError[] { new Error("Error 1 Message"), new Error("Error 2 Message") }.GetJoinedMessages()
                                                                                      .Should()
                                                                                      .Be("Error 1 Message;Error 2 Message");

    [Fact]
    public void GetJoinedMessages_should_ReturnEmptyString_when_ErrorsAreNotSpecified()
        => Array.Empty<IError>().GetJoinedMessages()
                                .Should()
                                .BeEmpty();

    [Fact]
    public void GetJoinedMessages_should_ThrowArgumentNullException_when_ErrorsAreNull()
    {
        // Arrange
        IEnumerable<IError> collection = null!;

        // Act
        Action action = () => collection.GetJoinedMessages();

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}