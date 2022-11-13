namespace Penzle.Core.Tests.Security;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Security))]
public sealed class BearerCredentialsShould
{
    [Fact]
    public void Construct_BearerCredentials_With_Proper_AuthenticationType_Bearer()
    {
        // Arrange
        var mock = new Mock<Credentials>();

        // Act
        mock.SetupAllProperties()
            .SetupProperty(property: credentials => credentials.AuthenticationType, initialValue: Enum.Parse<AuthenticationType>("Bearer"));

        // Assert
        mock.Object.AuthenticationType.Should().Be(AuthenticationType.Bearer);
    }

    [Fact]
    public void Construct_BearerCredentials_With_Set_Proper_AuthenticationType_Bearer()
    {
        // Arrange
        var mock = new Mock<Credentials>();

        // Act
        mock.SetupAllProperties();
        mock.Object.AuthenticationType = Enum.Parse<AuthenticationType>("Bearer");

        // Assert
        mock.Object.AuthenticationType.Should().Be(AuthenticationType.Bearer);
    }

    [Fact]
    public void Set_Delivery_And_Management_Key_Properly()
    {
        // Act
        const string ApiDeliveryKey = "6a15d038f0d443fe84a10e001579a7ea";
        const string ApiManagementKey = "41ba2bdc160a4d63ac9c510b13870bcd";

        // Arrange
        var credentials = new BearerCredentials(apiDeliveryKey: ApiDeliveryKey, apiManagementKey: ApiManagementKey);

        // Assert
        credentials.ApiDeliveryKey.Should().NotBeNullOrWhiteSpace();
        credentials.ApiDeliveryKey.Should().Be(expected: ApiDeliveryKey);
        credentials.ApiManagementKey.Should().NotBeNullOrWhiteSpace();
        credentials.ApiManagementKey.Should().Be(expected: ApiManagementKey);
    }

    [Fact]
    public void New_Object_Has_AuthenticationType_Bearer()
    {
        // Act
        var sut = new BearerCredentials(apiDeliveryKey: string.Empty, apiManagementKey: string.Empty);

        // Arrange
        var authenticationType = sut.AuthenticationType;

        // Assert
        authenticationType.Should().Be(expected: AuthenticationType.Bearer);
    }
}
