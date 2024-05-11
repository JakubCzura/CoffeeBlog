using AuthService.Application.ExtensionMethods.Collections;
using FluentAssertions;
using FluentResults;

namespace AuthService.Application.UnitTests.ExtensionMethods.Collections;

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
    {
        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_NoElementIsNullOrWhiteSpace()
    {
        // Arrange
        IEnumerable<string> collection = ["a", "b", "c", "d", "e"];

        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsAnyElementNullOrWhiteSpace_should_ReturnFalse_when_CollectionIsEmpty()
    {
        // Arrange
        IEnumerable<string> collection = [];

        // Act
        bool result = collection.IsAnyElementNullOrWhiteSpace();

        // Assert
        result.Should().BeFalse();
    }

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

    public static TheoryData<IError[], char, string> GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_Data
        => new()
        {
            { new IError[] { new Error("Error 1 Message"), new Error("Error 2 Message") }, ';', "Error 1 Message;Error 2 Message" },
            { new IError[] { new Error("Error 1 Message"), new Error("Error 2 Message") }, ',', "Error 1 Message,Error 2 Message" },
            { new IError[] { new Error("Error 1"), new Error("Error 2") }, ' ', "Error 1 Error 2" }
        };


    [Theory]
    [MemberData(nameof(GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_Data))]
    public void GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStrings_when_ErrorsAreSpecified(IError[] errors, char delimiter, string expected)
    {
        // Act
        string result = errors.GetJoinedMessages(delimiter);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GetJoinedMessages_should_ReturnJoinedMessagesOfErrorsAsStringsWithDefaultDelimiter_when_DelimiterIsNotSpecified()
    {
        // Arrange
        IEnumerable<IError> errors = [new Error("Error 1 Message"), new Error("Error 2 Message")];

        // Act
        string result = errors.GetJoinedMessages();

        // Assert
        result.Should().Be("Error 1 Message;Error 2 Message");
    }

    [Fact]
    public void GetJoinedMessages_should_ReturnEmptyString_when_ErrorsAreNotSpecified()
    {
        // Arrange
        IEnumerable<IError> errors = [];

        // Act
        string result = errors.GetJoinedMessages();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetJoinedMessages_should_ThrowArgumentNullException_when_ErrorsAreNull()
    {
        // Arrange
        IEnumerable<IError> errors = null!;

        // Act
        Action action = () => errors.GetJoinedMessages();

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }
}