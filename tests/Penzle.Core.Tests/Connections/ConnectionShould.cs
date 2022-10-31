// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using FluentAssertions;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Models;

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

    [Fact]
    public void Construct_Connection_With_Custom_Settings()
    {
        // Arrange
        var baseAddress = new Uri("https://api.penzle.com");
        var apiOptions = new ApiOptions(project: "main", environment: "staging");
        var credentialStore = new InMemoryCredentialStore(new BearerCredentials(apiDeliveryKey: "03e7d5fe3cbe", apiManagementKey: "b3d5fe3cbe03e"));
        var httpClientAdapter = new HttpClientAdapter(new HttpClient());
        var jsonSerializer = new MicrosoftJsonSerializer();

        // Act
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer);

        // Assert
        connection.Should().NotBeNull();
        connection.Should().BeAssignableTo<IConnection>();
        connection.Credentials.Should().BeOfType<BearerCredentials>();
        connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
        connection.HttpClient.Should().BeAssignableTo<HttpClientAdapter>();
        connection.HttpClient.HttpClient.Should().NotBeNull();
    }
}
