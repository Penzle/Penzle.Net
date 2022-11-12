// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core.Exceptions;

namespace Penzle.Core.Tests.Connections;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Connections))]
public class ConnectionShould
{
    [Fact]
    public void Construct_Connection_With_Default_Settings()
    {
        // Arrange
        var connectionStu = new Connection();

        // Act
        var connection = connectionStu as IConnection;

        // Assert
        connection.Should().NotBeNull();
        connection.Should().BeAssignableTo<IConnection>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public void Construct_Connection_With_Custom_Settings(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Assert
        connection.Should().NotBeNull();
        connection.Should().BeAssignableTo<IConnection>();
        connection.Credentials.Should().BeOfType<BearerCredentials>();
        connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
        connection.HttpClient.Should().BeAssignableTo<HttpClientAdapter>();
        connection.HttpClient.HttpClient.Should().NotBeNull();
    }

    [Theory]
    [ConnectionDependenciesData]
    public void Construct_Connection_With_Custom_Settings_Should_Protect_If_Not_AbsoluteUri(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        baseAddress = new Uri(uriString: "/endpoint", uriKind: UriKind.Relative);

        // Act
        Action connection = () =>
            new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Assert
        connection.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Connection_Have_To_Implemented_UserAgent()
    {
        // Arrange
        const string UserAgent = "Penzle/1.0.0";
        var connection = new Connection
        {
            UserAgent = UserAgent
        };

        // Assert
        connection.Should().NotBeNull();
        connection.UserAgent.Should().NotBeNull();
        connection.UserAgent.Should().Be(UserAgent);
    }

    [Fact]
    public void Connection_Have_To_Implemented_BaseAddress()
    {
        // Arrange
        const string ApiUri = "https://api.penzle.com/";
        var baseAddress = new Uri(ApiUri);
        var connection = new Connection
        {
            BaseAddress = baseAddress
        };

        // Assert
        connection.Should().NotBeNull();
        connection.BaseAddress.Should().NotBeNull();
        connection.BaseAddress.Should().Be(ApiUri);
    }


    [Theory]
    [ConnectionDependenciesData]
    public void Ability_To_Handle_Connection_Errors_When_Bad_Request_Occurred_Throw_PenzleException(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var mockResponse = new Mock<IResponse>();
        mockResponse
            .SetupAllProperties()
            .SetupProperty(property: response => response.StatusCode, initialValue: HttpStatusCode.BadRequest);

        // Act
        var action = () => connection.HandleErrors(mockResponse.Object);

        // Assert
        action.Should().Throw<PenzleException>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public void Ability_To_Handle_Errors_When_Not_Found_Resources_Should_Exit_From_Execution(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var mockResponse = new Mock<IResponse>();
        mockResponse
            .SetupAllProperties()
            .SetupProperty(property: response => response.StatusCode, initialValue: HttpStatusCode.NotFound);

        // Act
        var action = () => connection.HandleErrors(mockResponse.Object);

        // Assert
        action.Should().NotThrow<PenzleException>();
    }
}
