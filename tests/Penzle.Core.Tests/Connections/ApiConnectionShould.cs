using System.Net;
using FluentAssertions;
using Moq;
using Penzle.Core.Authentication;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;

namespace Penzle.Core.Tests.Connections;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Connections))]
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
            .SetupProperty(property: connection => connection.HttpClient, initialValue: new HttpClientAdapter(getHandler: () => new RedirectHandler()));
    }

    [Fact]
    public void Create_Api_Connection()
    {
        // Arrange
        var apiConnection = new ApiConnection();

        // Assert
        apiConnection.Should().NotBeNull();
    }

    [Fact]
    public void Create_Api_Connection_With_Connection_Multiplexer()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);

        // Act
        var connection = apiConnection.Connection;

        // Assert
        connection.Should().NotBeNull();
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

        apiConnection.Connection.CredentialStore.Should().NotBeNull();
        apiConnection.Connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();

        apiConnection.Connection.HttpClient.Should().NotBeNull();
        apiConnection.Connection.HttpClient.Should().BeOfType<HttpClientAdapter>();
    }

    [Fact]
    public async Task Fetch_Resource_With_Generic_Parameters_With_Status_Ok()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        const string ContentType = "text/plain";

        _mockConnection.Setup(expression: connection => connection.Get<Response>(
                It.IsAny<Uri>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => new Response(statusCode: HttpStatusCode.OK, body: "OK", headers: new Dictionary<string, string>(), contentType: ContentType));

        // Act
        var response = await apiConnection.Get<Response>(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().NotBeNull();
        response.ContentType.Should().Be(expected: ContentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(expected: HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resources_With_Generic_Responses()
    {
        // Arrange
        const string ContentType = "text/plain";
        const string ExpectedBody = "OK";

        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Put<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () =>
                new Response(statusCode: HttpStatusCode.NoContent, body: ExpectedBody, headers: new Dictionary<string, string>(), contentType: ContentType));

        // Act
        var response = await apiConnection.Put<Response>(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expected: ExpectedBody);
        response.ContentType.Should().Be(expected: ContentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resources_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Put(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Put(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(expected: HttpStatusCode.NoContent);
    }


    [Fact]
    public async Task To_Be_Able_To_Update_Resource_Fields_With_Generic_Responses()
    {
        // Arrange
        const string ContentType = "text/plain";
        const string ExpectedBody = "OK";

        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Patch<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () =>
                new Response(statusCode: HttpStatusCode.NoContent, body: ExpectedBody, headers: new Dictionary<string, string>(), contentType: ContentType));

        // Act
        var response = await apiConnection.Patch<Response>(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expected: ExpectedBody);
        response.ContentType.Should().Be(expected: ContentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resource_Fields_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Patch(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Patch(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Create_Resource_With_Generic_Responses()
    {
        // Arrange
        const string ContentType = "text/plain";
        const string ExpectedBody = "007C51FE-6680-4D6F-82AB-5D5D2D2B1B4F";

        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Post<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () =>
                new Response(statusCode: HttpStatusCode.OK, body: ExpectedBody, headers: new Dictionary<string, string>(), contentType: ContentType));

        // Act
        var response = await apiConnection.Post<Response>(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expected: ExpectedBody);
        response.ContentType.Should().Be(expected: ContentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(expected: HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Create_Resource_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Post(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Post(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Delete_Resource_With_Generic_Responses()
    {
        // Arrange
        const string ContentType = "text/plain";
        const string ExpectedBody = "501121C7-D310-4330-9D86-4B94BF2FA74D";

        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Delete<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () =>
                new Response(statusCode: HttpStatusCode.OK, body: ExpectedBody, headers: new Dictionary<string, string>(), contentType: ContentType));

        // Act
        var response = await apiConnection.Delete<Response>(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expected: ExpectedBody);
        response.ContentType.Should().Be(expected: ContentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(expected: HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Delete_Resource_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(connection: _mockConnection.Object);
        _mockConnection.Setup(expression: connection => connection.Delete(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Delete(uri: new Uri(uriString: "api/endpoint", uriKind: UriKind.Relative),
            body: new object(), parameters: new Dictionary<string, string>(), accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(expected: HttpStatusCode.NoContent);
    }
}
