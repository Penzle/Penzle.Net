using System.Net;
using FluentAssertions;
using Moq;
using Penzle.Core.Authentication;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Tests.Attribute;

namespace Penzle.Core.Tests.Connections;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Connections))]
public class ConnectionInterfaceShould
{
    [Fact]
    public void The_BaseAddress_Has_To_Be_Populated_With_Secured_TLS()
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu.SetupAllProperties();
        stu.SetupGet(expression: connection => connection.BaseAddress).Returns(valueFunction: () => new Uri(uriString: "https://api.penzle.com"));

        // Act
        var result = stu.Object.BaseAddress;

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(expected: "https://api.penzle.com");
        result.Scheme.StartsWith(value: "https").Should().BeTrue();
    }

    [Fact]
    public void The_In_Memory_CredentialStore_Must_Be_Assigned()
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu.SetupAllProperties();
        stu.SetupGet(expression: connection => connection.CredentialStore).Returns(valueFunction: () => new InMemoryCredentialStore(credentials: new BearerCredentials(apiDeliveryKey: string.Empty, apiManagementKey: string.Empty)));

        // Act
        var result = stu.Object.CredentialStore;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<InMemoryCredentialStore>();
    }

    [Fact]
    public void The_Credentials_Must_Be_Assigned_As_Bearer_Type()
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu.SetupAllProperties();
        stu.SetupGet(expression: connection => connection.Credentials).Returns(valueFunction: () => new BearerCredentials(apiDeliveryKey: string.Empty, apiManagementKey: string.Empty));

        // Act
        var result = stu.Object.Credentials;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Credentials>();
        result.Should().BeOfType<BearerCredentials>();
        result.AuthenticationType.Should().Be(expected: AuthenticationType.Bearer);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Updating_Non_Generic_Resource_Fields(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Patch(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);
        // Act
        var result = await stu.Object.Patch(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: headers, accepts: string.Empty, contentType: string.Empty, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Updating_Generic_Resource_Fields(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Patch<Response>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => new Response(statusCode: HttpStatusCode.NoContent, body: new object(), headers: headers, contentType: string.Empty));

        // Act
        var result = await stu.Object.Patch<Response>(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: null, accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Creation_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Post<Response>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => new Response(statusCode: HttpStatusCode.OK, body: new object(), headers: headers, contentType: string.Empty));

        // Act
        var result = await stu.Object.Post<Response>(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: null, accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(expected: HttpStatusCode.OK);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_None_Generic_For_Creation_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Post(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.OK);
        // Act
        var result = await stu.Object.Post(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: headers, accepts: string.Empty, contentType: string.Empty, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().Be(expected: HttpStatusCode.OK);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Update_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Put<Response>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => new Response(statusCode: HttpStatusCode.NoContent, body: new object(), headers: headers, contentType: string.Empty));

        // Act
        var result = await stu.Object.Put<Response>(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: null, accepts: null, contentType: null, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_None_Generic_For_Update_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu
            .Setup(expression: connection => connection.Put(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(valueFunction: () => HttpStatusCode.NoContent);
        // Act
        var result = await stu.Object.Put(uri: new Uri(uriString: "https://api.penzle.com"), body: new object(), parameters: headers, accepts: string.Empty, contentType: string.Empty, cancellationToken: CancellationToken.None);

        // Assert
        result.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public void Ability_To_Set_Time_Out()
    {
        // Arrange
        var stu = new Mock<IConnection>();
        stu.Setup(expression: connection => connection.SetRequestTimeout(It.IsAny<TimeSpan>()));

        // Act
        stu.Object.SetRequestTimeout(timeout: TimeSpan.FromSeconds(value: 10));

        // Assert
        stu.Verify(expression: connection => connection.SetRequestTimeout(TimeSpan.FromSeconds(10)), times: Times.Once);
    }
}