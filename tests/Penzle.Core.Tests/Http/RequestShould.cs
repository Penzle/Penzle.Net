﻿// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Http;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Http))]
public class RequestShould
{
    [Fact]
    public void Construct_Request()
    {
        // Arrange
        var request = new Request();

        // Act
        var result = request as IRequest;

        // Assert
        result.Should().NotBeNull();
        request.Should().BeAssignableTo<IRequest>();
        request.Headers.Should().NotBeNull();
        request.Parameters.Should().NotBeNull();
        request.Timeout.Should().Be(expected: TimeSpan.FromMinutes(value: 2));
    }

    [Fact]
    public void Ability_To_Set_Request_Generic_Body()
    {
        // Arrange
        var request = new Request
        {
            Body = new StreamContent(content: Stream.Null)
        };

        // Assert
        request.Should().NotBeNull();
        request.Body.Should().NotBeNull();
        request.Body.Should().BeOfType<StreamContent>();
    }

    [Fact]
    public void Ability_To_Set_Request_Http_Method_Get()
    {
        // Arrange
        var request = new Request
        {
            Method = HttpMethod.Get
        };

        // Assert
        request.Should().NotBeNull();
        request.Method.Should().Be(expected: HttpMethod.Get);
    }

    [Fact]
    public void Ability_To_Set_Request_Base_Address()
    {
        // Arrange
        const string BaseAddress = "https://api.penzle.com/";
        var request = new Request
        {
            BaseAddress = new Uri(uriString: BaseAddress)
        };

        // Assert
        request.Should().NotBeNull();
        request.BaseAddress.Should().Be(expected: BaseAddress);
    }

    [Fact]
    public void Ability_To_Set_Request_Endpoint()
    {
        // Arrange
        const string Endpoint = "api/v1/endpoint";
        var request = new Request
        {
            Endpoint = new Uri(uriString: Endpoint, uriKind: UriKind.Relative)
        };

        // Assert
        request.Should().NotBeNull();
        request.Endpoint.Should().Be(expected: Endpoint);
    }

    [Fact]
    public void Ability_To_Set_Request_ContentType()
    {
        // Arrange
        const string ContentType = "application/json";
        var request = new Request
        {
            ContentType = ContentType
        };

        // Assert
        request.Should().NotBeNull();
        request.ContentType.Should().Be(expected: ContentType);
    }

    [Fact]
    public void Ability_To_Set_Request_Headers()
    {
        // Arrange
        var request = new Request
        {
            Headers = new Dictionary<string, string>
            {
                {
                    "key", "value"
                }
            }
        };

        // Assert
        request.Should().NotBeNull();
        request.Headers.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Ability_To_Set_Request_Parameters()
    {
        // Arrange
        var request = new Request
        {
           Parameters = new Dictionary<string, string>()
           {
                {
                     "key", "value"
                }
           }
        };

        // Assert
        request.Should().NotBeNull();
        request.Parameters.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Ability_To_Set_Request_Timeout()
    {
        // Arrange
        var request = new Request
        {
           Timeout = TimeSpan.FromSeconds(60)
        };

        // Assert
        request.Should().NotBeNull();
        request.Timeout.Should().BeGreaterOrEqualTo(TimeSpan.FromMinutes(1));
    }
    
    [Fact]
    public void Test()
    {
        // Arrange
        var request = new Request
        {
            Timeout = TimeSpan.FromSeconds(60)
        };

        // Assert
        request.Should().NotBeNull();
        request.Timeout.Should().BeGreaterOrEqualTo(TimeSpan.FromMinutes(1));
    }
}
