// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Reflection;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Models;
using Xunit.Sdk;

namespace Penzle.Core.Tests.Attribute;

public sealed class ConnectionDependenciesDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return new[]
        {
            new object[]
            {
                new Uri("https://api.penzle.com"), new ApiOptions(project: "main", environment: "staging"), new InMemoryCredentialStore(new BearerCredentials(apiDeliveryKey: "54573d95", apiManagementKey: "5d954573")), new HttpClientAdapter(new HttpClient()), new MicrosoftJsonSerializer(), new SdkPlatformInformation()
            }
        };
    }
}
