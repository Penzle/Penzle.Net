// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Utilities;

public class GuardShould
{
    [Fact]
    public void Ensure_That_Values_Are_Protected_From_Empty_Strings()
    {
        // Arrange
        const string Value = "This is a test";

        // Act
        var handler = () => Guard.ArgumentNotNullOrEmptyString(value: Value, name: nameof(Value));

        // Assert
        handler.Should().NotThrow();
    }

    [Fact]
    public void Ensure_That_Values_Are_Protected_From_Empty_Strings_If_Null()
    {
        // Arrange
        string value = null;

        // Act
        var handler = () => Guard.ArgumentNotNullOrEmptyString(value: value, name: nameof(value));

        // Assert
        handler.Should().Throw<ArgumentException>().And.Message.Should().Be("Value cannot be null. (Parameter 'value')");
    }

    [Fact]
    public void Ensure_That_Values_Are_Protected_From_Empty_Strings_If_Empty_Space()
    {
        // Arrange
        const string Value = " ";

        // Act
        var handler = () => Guard.ArgumentNotNullOrEmptyString(value: Value, name: nameof(Value));

        // Assert
        handler.Should().Throw<ArgumentException>().And.Message.Should().Be("String cannot be empty (Parameter 'Value')");
    }
}
