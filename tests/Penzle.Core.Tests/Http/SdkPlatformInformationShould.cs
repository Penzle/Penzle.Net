// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Http;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Platform))]
public class SdkPlatformInformationShould
{
    [Fact]
    public void Ability_To_Return_Unknown_Platform()
    {
        // Arrange
        var platform = new SdkPlatformInformation();

        // Act
        var result = platform.PlatformInformation;

        // Assert
        result.Should().Be("Unknown Platform");
    }

    [Fact]
    public void Ability_To_Set_Platform()
    {
        // Arrange
        var platform = new SdkPlatformInformation();

        // Act
        platform.PlatformInformation = "Test Platform";

        // Assert
        platform.PlatformInformation.Should().Be("Test Platform");
    }

    [Fact]
    public void Ability_To_Return_Platform_Information_If_Empty()
    {
        // Arrange
        var platform = new SdkPlatformInformation();

        // Act
        platform.PlatformInformation = null;

        // Assert
        platform.PlatformInformation.Should().BeNull();
    }

    [Fact]
    public void Ability_To_Return_Platform_Information_Must_Be_Known()
    {
        // Arrange
        var platform = new SdkPlatformInformation
        {
            PlatformInformation = null
        };

        // Act
        var platformInformation = platform.GetPlatformInformation();

        // Assert
        platformInformation.Should().NotBeNullOrWhiteSpace();
    }
}
