using FluentAssertions;
using Moq;
using Penzle.Core.Authentication;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;

namespace Penzle.Core.Tests.Connections;

public class ApiConnectionShould
{
    private readonly BearerCredentials _credentials;
    private readonly Mock<IConnection> _mockConnection;

    public ApiConnectionShould()
    {
        _credentials = new BearerCredentials(apiDeliveryKey: "ebad3c9e602b4ea08cb893a3ff4de2a4", apiManagementKey: "4708fb06ed68440e9c8f3f14b62a790a");
        _mockConnection = new Mock<IConnection>()
            .SetupAllProperties()
            .SetupProperty(property: connection => connection.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"))
            .SetupProperty(property: connection => connection.CredentialStore, initialValue: new InMemoryCredentialStore(credentials: _credentials))
            .SetupProperty(property: connection => connection.Credentials, initialValue: _credentials)
            .SetupProperty(property: connection => connection.UserAgent, initialValue: "Penzle.Core.Tests/1.0.0")
            .SetupProperty(property: connection => connection.HttpClient, initialValue: new HttpClientAdapter(getHandler: () => new RedirectHandler()))
            .SetupProperty(property: connection => connection.PlatformInformation, initialValue: "Penzle.Net-1.0.0");
    }

    [Fact]
    public void The_Connection_Must_Be_Assigned_To_The_Api_Adapter()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);

        // Act
        var bearerCredentials = apiConnection.Connection.Credentials as BearerCredentials;

        // Assert
        bearerCredentials!.AuthenticationType.Should().Be(expected: AuthenticationType.Bearer);
        bearerCredentials.ApiDeliveryKey.Should().Be(expected: _credentials.ApiDeliveryKey);
        bearerCredentials.ApiManagementKey.Should().Be(expected: _credentials.ApiManagementKey);

        apiConnection.Connection.BaseAddress.Should().Be(expected: new Uri(uriString: "https://api.penzle.com"));
        apiConnection.Connection.UserAgent.Should().Be(expected: "Penzle.Core.Tests/1.0.0");
        apiConnection.Connection.PlatformInformation.Should().Be(expected: "Penzle.Net-1.0.0");
        
        apiConnection.Connection.CredentialStore.Should().NotBeNull();
        apiConnection.Connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
        
        apiConnection.Connection.HttpClient.Should().NotBeNull();
        apiConnection.Connection.HttpClient.Should().BeOfType<HttpClientAdapter>();
    }
}