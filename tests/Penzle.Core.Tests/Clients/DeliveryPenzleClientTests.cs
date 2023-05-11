// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.
using Xunit;
using Moq;
using FluentAssertions;
using Penzle.Core;
using System;

namespace Penzle.Core.Tests.Clients
{
    public class DeliveryPenzleClientTests
    {
        private Mock<IConnection> _mockConnection;
        private Mock<IHttpClient> _mockHttpClient;
        private Mock<IJsonSerializer> _mockJsonSerializer;

        public DeliveryPenzleClientTests()
        {
            _mockConnection = new Mock<IConnection>();
            _mockHttpClient = new Mock<IHttpClient>();
            _mockJsonSerializer = new Mock<IJsonSerializer>();
        }

        [Fact]
        public void Factory_CreateInstance_Succeeds()
        {
            // Arrange
            var apiDeliveryKey = "testKey";
            Action<ApiOptions> apiOptions = options => options.Environment = "TestEnvironment";
            var baseAddress = new Uri("http://localhost");

            // Act
            var client = DeliveryPenzleClient.Factory(apiDeliveryKey, apiOptions, _mockHttpClient.Object, _mockJsonSerializer.Object, TimeSpan.FromSeconds(30), baseAddress);

            // Assert
            client.Should().NotBeNull();
            client.Entry.Should().NotBeNull();
            client.Form.Should().NotBeNull();
            client.Template.Should().NotBeNull();
            client.Asset.Should().NotBeNull();
        }

        [Fact]
        public void Factory_With_ApiOptions_Succeeds()
        {
            // Arrange
            var apiDeliveryKey = "testKey";
            Action<ApiOptions> apiOptions = options => options.Environment = "TestEnvironment";
            var baseAddress = new Uri("http://localhost");

            // Act
            var client = DeliveryPenzleClient.Factory(apiDeliveryKey, apiOptions, baseAddress);
            
            // Assert
            client.Should().NotBeNull();
            client.Entry.Should().NotBeNull();
            client.Form.Should().NotBeNull();
            client.Template.Should().NotBeNull();
            client.Asset.Should().NotBeNull();
        }

        [Fact]
        public void Factory_With_HttpClient_Succeeds()
        {
            // Arrange
            var apiDeliveryKey = "testKey";
            Action<ApiOptions> apiOptions = options => options.Environment = "TestEnvironment";
            var baseAddress = new Uri("http://localhost");

            // Act
            var client = DeliveryPenzleClient.Factory(apiDeliveryKey, apiOptions, _mockHttpClient.Object, baseAddress);

            // Assert
            client.Should().NotBeNull();
            client.Entry.Should().NotBeNull();
            client.Form.Should().NotBeNull();
            client.Template.Should().NotBeNull();
            client.Asset.Should().NotBeNull();
        }

        [Fact]
        public void Factory_With_JsonSerializer_Succeeds()
        {
            // Arrange
            var apiDeliveryKey = "testKey";
            Action<ApiOptions> apiOptions = options => options.Environment = "TestEnvironment";
            var baseAddress = new Uri("http://localhost");

            // Act
            var client = DeliveryPenzleClient.Factory(apiDeliveryKey, apiOptions, _mockHttpClient.Object, _mockJsonSerializer.Object, baseAddress);

            // Assert
            client.Should().NotBeNull();
            client.Entry.Should().NotBeNull();
            client.Form.Should().NotBeNull();
            client.Template.Should().NotBeNull();
            client.Asset.Should().NotBeNull();
        }

        [Fact]
        public void Factory_With_TimeOut_Succeeds()
        {
            // Arrange
            var apiDeliveryKey = "testKey";
            Action<ApiOptions> apiOptions = options => options.Environment = "TestEnvironment";
            var baseAddress = new Uri("http://localhost");
            var timeOut = TimeSpan.FromSeconds(30);

            // Act
            var client = DeliveryPenzleClient.Factory(apiDeliveryKey, apiOptions, _mockHttpClient.Object, _mockJsonSerializer.Object, timeOut, baseAddress);

            // Assert
            client.Should().NotBeNull();
            client.Entry.Should().NotBeNull();
            client.Form.Should().NotBeNull();
            client.Template.Should().NotBeNull();
            client.Asset.Should().NotBeNull();
        }
    }
}
