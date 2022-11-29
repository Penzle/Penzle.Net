// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Attribute;

public sealed class ConnectionDependenciesDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var platformInformationMock = new Mock<IPlatformInformation>();
        platformInformationMock.Setup(information => information.GetPlatformInformation()).Returns("1.0.0");

        return new[]
        {
            new object[]
            {
                new Uri("https://api.penzle.com"), new ApiOptions(project: "main", environment: "staging"), new InMemoryCredentialStore(new BearerCredentials(apiDeliveryKey: "54573d95", apiManagementKey: "5d954573")), new HttpClientAdapter(new HttpClient()), new MicrosoftJsonSerializer(), platformInformationMock.Object
            }
        };
    }
}
