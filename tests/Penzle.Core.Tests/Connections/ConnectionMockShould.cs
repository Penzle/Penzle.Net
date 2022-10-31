using System.Net;
using System.Reflection;
using FluentAssertions;
using Moq;
using Penzle.Core.Authentication;
using Penzle.Core.Exceptions;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Tests.Attribute;

namespace Penzle.Core.Tests.Connections;

[Trait(nameof(TraitDefinitions.Category), nameof(TraitDefinitions.Connections))]
public class ConnectionMockShould
{
    [Fact]
    public void The_BaseAddress_Has_To_Be_Populated_With_Secured_TLS()
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu.SetupAllProperties();
        stu.SetupGet(connection => connection.BaseAddress).Returns(() => new Uri("https://api.penzle.com"));

        // Act
        var result = stu.Object.BaseAddress;

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("https://api.penzle.com");
        result.Scheme.StartsWith("https").Should().BeTrue();
    }

    [Fact]
    public void The_In_Memory_CredentialStore_Must_Be_Assigned()
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu.SetupAllProperties();
        stu.SetupGet(connection => connection.CredentialStore).Returns(() =>
            new InMemoryCredentialStore(new BearerCredentials(string.Empty, string.Empty)));

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
        var stu = new Mock<Connection>();
        stu.SetupAllProperties();
        stu.SetupGet(connection => connection.Credentials)
            .Returns(() => new BearerCredentials(string.Empty, string.Empty));

        // Act
        var result = stu.Object.Credentials;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<Credentials>();
        result.Should().BeOfType<BearerCredentials>();
        result.AuthenticationType.Should().Be(AuthenticationType.Bearer);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Updating_Non_Generic_Resource_Fields(
        IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Patch(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => HttpStatusCode.NoContent);
        // Act
        var result = await stu.Object.Patch(new Uri("https://api.penzle.com"), new object(), headers, string.Empty,
            string.Empty, CancellationToken.None);

        // Assert
        result.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Updating_Generic_Resource_Fields(
        IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Patch<Response>(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Response(HttpStatusCode.NoContent, new object(), headers, string.Empty));

        // Act
        var result = await stu.Object.Patch<Response>(new Uri("https://api.penzle.com"), new object(), null, null, null,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Creation_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Post<Response>(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Response(HttpStatusCode.OK, new object(), headers, string.Empty));

        // Act
        var result = await stu.Object.Post<Response>(new Uri("https://api.penzle.com"), new object(), null, null, null,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_None_Generic_For_Creation_Resource(
        IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Post(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => HttpStatusCode.OK);
        // Act
        var result = await stu.Object.Post(new Uri("https://api.penzle.com"), new object(), headers, string.Empty,
            string.Empty, CancellationToken.None);

        // Assert
        result.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_For_Update_Resource(IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Put<Response>(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new Response(HttpStatusCode.NoContent, new object(), headers, string.Empty));

        // Act
        var result = await stu.Object.Put<Response>(new Uri("https://api.penzle.com"), new object(), null, null, null,
            CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [HeadersData]
    public async Task Must_Pass_Checks_Before_Send_Request_None_Generic_For_Update_Resource(
        IDictionary<string, string> headers)
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu
            .Setup(connection => connection.Put(It.IsAny<Uri>(), It.IsAny<object>(),
                It.IsAny<IDictionary<string, string>>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => HttpStatusCode.NoContent);
        // Act
        var result = await stu.Object.Put(new Uri("https://api.penzle.com"), new object(), headers, string.Empty,
            string.Empty, CancellationToken.None);

        // Assert
        result.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public void Ability_To_Set_Time_Out()
    {
        // Arrange
        var stu = new Mock<Connection>();
        stu.Setup(connection => connection.SetRequestTimeout(It.IsAny<TimeSpan>()));

        // Act
        stu.Object.SetRequestTimeout(TimeSpan.FromSeconds(10));

        // Assert
        stu.Verify(connection => connection.SetRequestTimeout(TimeSpan.FromSeconds(10)), Times.Once);
    }

    [Fact]
    public void Ability_To_Handle_Errors_When_Bad_Request_Occurred_Throw_PenzleException()
    {
        // Arrange
        var mockResponse = new Mock<IResponse>();
        mockResponse
            .SetupAllProperties()
            .SetupProperty(response => response.StatusCode, HttpStatusCode.BadRequest);

        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.HandleErrors(mockResponse.Object))
            .Throws<PenzleException>();

        // Act
        var handleErrors = () => mockConnection.Object.HandleErrors(mockResponse.Object);

        // Assert
        handleErrors.Should().Throw<PenzleException>();
    }

    [Fact]
    public void Ability_To_Handle_Errors_When_Not_Found_Resources_Should_Exit_From_Execution()
    {
        // Arrange
        var mockResponse = new Mock<IResponse>();
        mockResponse
            .SetupAllProperties()
            .SetupProperty(response => response.StatusCode, HttpStatusCode.NotFound);

        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.HandleErrors(mockResponse.Object))
            .Verifiable();

        // Act
        var handleErrors = () => mockConnection.Object.HandleErrors(mockResponse.Object);

        // Assert
        handleErrors.Should().NotThrow<PenzleException>();
    }

    [Fact]
    public void Display_The_Current_Version_Fallback_Of_SDK()
    {
        // Arrange
        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.GetVersionInformation())
            .Returns("1.0.0");

        // Act
        var version = mockConnection.Object.GetVersionInformation();

        // Assert
        version.Should().Be("1.0.0");
    }

    [Fact]
    public void Display_The_Current_Version_From_Assembly_Of_SDK()
    {
        // Arrange
        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.GetVersionInformation())
            .Returns(() => Assembly.GetExecutingAssembly().GetName().Version!.ToString());

        // Act
        var version = mockConnection.Object.GetVersionInformation();

        // Assert
        version.Should().Be("2.0.1.0");
    }

    [Fact]
    public void Display_The_Current_Culture_Info_Of_SDK()
    {
        // Arrange
        const string CurrentCulture = "en-US";
        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.GetCultureInformation())
            .Returns(CurrentCulture);

        // Act
        var culture = mockConnection.Object.GetCultureInformation();

        // Assert
        culture.Should().Be(CurrentCulture);
    }

    [Fact]
    public void Display_The_Current_Platform_Information_Of_SDK()
    {
        // Arrange
        Environment.SetEnvironmentVariable("OS", "Windows_NT");
        const string CurrentPlatform = "Windows";
        var mockConnection = new Mock<Connection>();
        mockConnection
            .Setup(connection => connection.GetPlatformInformation())
            .Returns(CurrentPlatform);

        // Act
        var platform = mockConnection.Object.GetPlatformInformation();

        // Assert
        platform.Should().Be(CurrentPlatform);
    }
}
