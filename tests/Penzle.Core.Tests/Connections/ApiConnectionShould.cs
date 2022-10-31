using System.Net;
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
        _credentials = new BearerCredentials("ebad3c9e602b4ea08cb893a3ff4de2a4", "4708fb06ed68440e9c8f3f14b62a790a");
        _mockConnection = new Mock<IConnection>()
            .SetupAllProperties()
            .SetupProperty(connection => connection.BaseAddress, new Uri("https://api.penzle.com"))
            .SetupProperty(connection => connection.CredentialStore, new InMemoryCredentialStore(_credentials))
            .SetupProperty(connection => connection.Credentials, _credentials)
            .SetupProperty(connection => connection.UserAgent, "Penzle.Core.Tests/1.0.0")
            .SetupProperty(connection => connection.HttpClient, new HttpClientAdapter(() => new RedirectHandler()))
            .SetupProperty(connection => connection.PlatformInformation, "Penzle.Net-1.0.0");
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
        var apiConnection = new ApiConnection(_mockConnection.Object);

        // Act
        var connection = apiConnection.Connection;

        // Assert
        connection.Should().NotBeNull();
    }

    [Fact]
    public void The_Connection_Must_Be_Assigned_To_The_Api_Adapter()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);

        // Act
        var bearerCredentials = apiConnection.Connection.Credentials as BearerCredentials;

        // Assert
        bearerCredentials!.AuthenticationType.Should().Be(AuthenticationType.Bearer);
        bearerCredentials.ApiDeliveryKey.Should().Be(_credentials.ApiDeliveryKey);
        bearerCredentials.ApiManagementKey.Should().Be(_credentials.ApiManagementKey);

        apiConnection.Connection.BaseAddress.Should().Be(new Uri("https://api.penzle.com"));
        apiConnection.Connection.UserAgent.Should().Be("Penzle.Core.Tests/1.0.0");
        apiConnection.Connection.PlatformInformation.Should().Be("Penzle.Net-1.0.0");

        apiConnection.Connection.CredentialStore.Should().NotBeNull();
        apiConnection.Connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();

        apiConnection.Connection.HttpClient.Should().NotBeNull();
        apiConnection.Connection.HttpClient.Should().BeOfType<HttpClientAdapter>();
    }

    [Fact]
    public async Task Fetch_Resource_With_Generic_Parameters_With_Status_Ok()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);
        const string contentType = "text/plain";

        _mockConnection.Setup(connection => connection.Get<Response>(
                It.IsAny<Uri>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Response(HttpStatusCode.OK, "OK", new Dictionary<string, string>(), contentType));

        // Act
        var response = await apiConnection.Get<Response>(new Uri("api/endpoint", UriKind.Relative),
            new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().NotBeNull();
        response.ContentType.Should().Be(contentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resources_With_Generic_Responses()
    {
        // Arrange
        const string contentType = "text/plain";
        const string expectedBody = "OK";

        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Put<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() =>
                new Response(HttpStatusCode.NoContent, expectedBody, new Dictionary<string, string>(), contentType));

        // Act
        var response = await apiConnection.Put<Response>(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expectedBody);
        response.ContentType.Should().Be(contentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resources_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Put(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Put(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }


    [Fact]
    public async Task To_Be_Able_To_Update_Resource_Fields_With_Generic_Responses()
    {
        // Arrange
        const string contentType = "text/plain";
        const string expectedBody = "OK";

        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Patch<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() =>
                new Response(HttpStatusCode.NoContent, expectedBody, new Dictionary<string, string>(), contentType));

        // Act
        var response = await apiConnection.Patch<Response>(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expectedBody);
        response.ContentType.Should().Be(contentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Update_Resource_Fields_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Patch(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Patch(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Create_Resource_With_Generic_Responses()
    {
        // Arrange
        const string contentType = "text/plain";
        const string expectedBody = "007C51FE-6680-4D6F-82AB-5D5D2D2B1B4F";

        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Post<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() =>
                new Response(HttpStatusCode.OK, expectedBody, new Dictionary<string, string>(), contentType));

        // Act
        var response = await apiConnection.Post<Response>(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expectedBody);
        response.ContentType.Should().Be(contentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Create_Resource_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Post(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Post(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task To_Be_Able_To_Delete_Resource_With_Generic_Responses()
    {
        // Arrange
        const string contentType = "text/plain";
        const string expectedBody = "501121C7-D310-4330-9D86-4B94BF2FA74D";

        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Delete<Response>(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() =>
                new Response(HttpStatusCode.OK, expectedBody, new Dictionary<string, string>(), contentType));

        // Act
        var response = await apiConnection.Delete<Response>(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Body.Should().Be(expectedBody);
        response.ContentType.Should().Be(contentType);
        response.Headers.Should().BeEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task To_Be_Able_To_Delete_Resource_With_None_Generic_Responses()
    {
        // Arrange
        var apiConnection = new ApiConnection(_mockConnection.Object);
        _mockConnection.Setup(connection => connection.Delete(
                It.IsAny<Uri>(),
                It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(() => HttpStatusCode.NoContent);

        // Act
        var response = await apiConnection.Delete(new Uri("api/endpoint", UriKind.Relative),
            new object(), new Dictionary<string, string>(), null, null, CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }
}