// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using FluentAssertions;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Models;
using Penzle.Core.Tests.Models;

namespace Penzle.Core.Tests.Integration;

public class ClientHttpIntegrationShould
{
    private readonly IDeliveryPenzleClient _client;

    public ClientHttpIntegrationShould()
    {
        var apiOptions = new ApiOptions(project: "penzleWebsite", environment: "development");
        var baseAddress = new Uri("https://api-cappy.penzle.com");
        var credentialStore = new InMemoryCredentialStore(new BearerCredentials(apiDeliveryKey: null, apiManagementKey: null));

        IConnection connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: new HttpClientAdapter(() => new HttpClientHandler()), serializer: new MicrosoftJsonSerializer());
        _client = new DeliveryPenzleClient(connection);
    }

    [Fact]
    public async Task GetContent()
    {
        var response = await _client.Entry.GetEntry<TabContainer>(new Guid("22182d07-1798-4b2e-8ced-1d1d2f9fbe9d"));
        response.Should().NotBeNull();
    }
}
