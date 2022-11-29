namespace Penzle.Core.Tests.Http;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Http))]
public class HttpClientAdapterShould
{
    [Fact]
    public void Construction_Http_Client_Adapter_With_Default_Settings()
    {
        // Arrange
        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var httpClient = httpClientAdapter.HttpClient;

        // Assert
        httpClient.Should().NotBeNull();
        httpClient.Timeout.Should().BePositive();
        httpClient.DefaultRequestHeaders.Should().NotBeNull();
    }

    [Fact]
    public void Construction_Http_Client_Adapter_With_Custom_Settings()
    {
        // Arrange
        var httpClientAdapter = new HttpClientAdapter(getHandler: () => new HttpClientHandler());

        // Act
        var httpClient = httpClientAdapter.HttpClient;

        // Assert
        httpClient.Should().NotBeNull();
        httpClient.Timeout.Should().Be(expected: new TimeSpan(days: 0, hours: 0, minutes: 1, seconds: 40, milliseconds: 0));
        httpClient.DefaultRequestHeaders.Should().NotBeNull();
    }

    [Fact]
    public void Ability_To_Define_Timeout_Of_Http_Client()
    {
        // Arrange
        var httpClientAdapter = new HttpClientAdapter();

        // Act
        httpClientAdapter.SetRequestTimeout(timeout: TimeSpan.FromSeconds(value: 60));

        // Assert
        httpClientAdapter.HttpClient.Should().NotBeNull();
        httpClientAdapter.HttpClient.Timeout.Should().Be(expected: new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 60, milliseconds: 0));
    }

    [Fact]
    public async Task Ability_Send_Http_Request_Using_Request_And_CancellationToken()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: "3a8ec539627a4f34ac7651ffe6024dbe");
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());

        var mockHttpClient = new Mock<HttpClient>();
        mockHttpClient.SetupAllProperties();

        mockHttpClient
            .Setup(expression: handler => handler.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(value: new HttpResponseMessage(statusCode: HttpStatusCode.NoContent));

        var httpClientAdapter = new HttpClientAdapter(httpClient: mockHttpClient.Object);

        // Act
        var response = await httpClientAdapter.Send(request: requestMock.Object, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(expected: HttpStatusCode.NoContent);
    }

    [Fact]
    public void Ability_Build_Http_Request_String_Message()
    {
        // Arrange
        const string InitialValue = "3a8ec539627a4f34ac7651ffe6024dbe";

        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: InitialValue);
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Content.Should().NotBeNull();
        response.Content.Should().BeOfType<StringContent>();
        response.Method.Should().Be(expected: HttpMethod.Post);
        using var reader = new StreamReader(stream: response.Content!.ReadAsStream(), encoding: Encoding.UTF8);
        reader.ReadToEnd().Should().Be(expected: InitialValue);
    }

    [Fact]
    public void Ability_Build_Http_Request_Stream_Message()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: new MemoryStream(buffer: Array.Empty<byte>()));
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Content.Should().NotBeNull();
        response.Content.Should().BeOfType<StreamContent>();
        response.Method.Should().Be(expected: HttpMethod.Post);
    }

    [Fact]
    public void Ability_Build_Http_Request_HttpContent_Message()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: new StreamContent(content: Stream.Null));
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Content.Should().NotBeNull();
        response.Content.Should().BeOfType<StreamContent>();
        response.Method.Should().Be(expected: HttpMethod.Post);
        response.Content!.ReadAsStream().Should().BeReadable();
    }

    [Fact]
    public void Ability_Build_Http_Request_MultipartFormDataContent_Message()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: new MultipartFormDataContent
        {
            {
                new StreamContent(content: Stream.Null), "document", "document.pdf"
            }
        });
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Content.Should().NotBeNull();
        response.Content.Should().BeOfType<MultipartFormDataContent>();
        response.Method.Should().Be(expected: HttpMethod.Post);
        using var content = response.Content as MultipartFormDataContent;
        content.Should().NotBeEmpty();
        content!.First().Headers.ContentDisposition!.Name.Should().Be(expected: "document");
        content!.First().Headers.ContentDisposition!.FileName.Should().Be(expected: "document.pdf");
    }

    [Fact]
    public void Ability_Build_Http_Request_Must_Protect_From_ArgumentNotNull()
    {
        // Arrange
        using var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = () => httpClientAdapter.BuildRequestMessage(request: null);

        // Assert
        response.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ability_Build_Http_Request_With_Custom_Headers()
    {
        // Arrange
        const string Key = "x-type-sdk";
        const string Value = "1.0.0";

        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: new MultipartFormDataContent
        {
            {
                new StreamContent(content: Stream.Null), "document", "document.pdf"
            }
        });
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>
        {
            {
                Key, Value
            }
        });

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Headers.Select(selector: pair => pair.Key).Should().Contain(expected: Key);
        response.Headers.Select(selector: pair => pair.Value).Should().Contain(predicate: c => c.Contains(Value));
    }

    [Fact]
    public void Ability_Build_Http_Request_Must_Protect_From_Any_Kind_Of_Exception()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: new MultipartFormDataContent
        {
            {
                new StreamContent(content: Stream.Null), "document", "document.pdf"
            }
        });
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: null);

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = () => httpClientAdapter.BuildRequestMessage(request: requestMock.Object);

        // Assert
        response.Should().Throw<Exception>();
    }

    [Fact]
    public void Ability_Build_Http_Response_Message_Must_Protect_Against_ArgumentNotNull()
    {
        // Arrange
        var httpClientAdapter = new HttpClientAdapter();

        // Act
        Func<Task<IResponse>> response = () => httpClientAdapter.BuildResponse(responseMessage: null);

        // Assert
        response.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Ability_Build_Http_Response_Message_Have_Custom_Headers()
    {
        // Arrange
        const string Key = "x-type-sdk";
        const string Value = "1.0.0";

        var httpResponseMessageMock = new Mock<HttpResponseMessage>();
        httpResponseMessageMock.SetupAllProperties();

        httpResponseMessageMock.Object.Content = new StringContent(content: "Hello World");
        httpResponseMessageMock.Object.Headers.Add(name: Key, value: Value);

        var httpClientAdapter = new HttpClientAdapter();

        // Act
        var response = await httpClientAdapter.BuildResponse(responseMessage: httpResponseMessageMock.Object);

        // Assert
        response.Should().NotBeNull();
        response.Headers.Select(selector: pair => pair.Key).Should().Contain(expected: Key);
        response.Headers.Select(selector: pair => pair.Value).Should().Contain(predicate: c => c.Contains(Value));
    }

    [Fact]
    public void Ability_To_Build_Cancellation_Token_Source_To_Cancel_Http_Request()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: "094dd1418a99453cb747fa39f6e77c76");
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());
        requestMock.SetupProperty(property: request => request.Timeout, initialValue: TimeSpan.Zero);

        var httpClientAdapter = new HttpClientAdapter();
        var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.Zero);

        // Act
        var response = httpClientAdapter.GetCancellationTokenForRequest(request: requestMock.Object, cancellationToken: cancellationTokenSource.Token);

        // Assert
        response.Should().NotBeNull();
        response.IsCancellationRequested.Should().BeTrue();
    }

    [Fact]
    public void Ability_To_Build_Cancellation_Token_From_Request_Timeout()
    {
        // Arrange
        var requestMock = new Mock<IRequest>();
        requestMock.SetupAllProperties();
        requestMock.SetupProperty(property: request => request.Body, initialValue: "094dd1418a99453cb747fa39f6e77c76");
        requestMock.SetupProperty(property: request => request.Method, initialValue: HttpMethod.Post);
        requestMock.SetupProperty(property: request => request.ContentType, initialValue: "application/json");
        requestMock.SetupProperty(property: request => request.BaseAddress, initialValue: new Uri(uriString: "https://api.penzle.com"));
        requestMock.SetupProperty(property: request => request.Endpoint, initialValue: new Uri(uriString: "api/v1/endpoint", uriKind: UriKind.Relative));
        requestMock.SetupProperty(property: request => request.Headers, initialValue: new Dictionary<string, string>());
        requestMock.SetupProperty(property: request => request.Timeout, initialValue: TimeSpan.FromMinutes(value: 1));

        var httpClientAdapter = new HttpClientAdapter();
        var cancellationTokenSource = new CancellationTokenSource(delay: TimeSpan.FromSeconds(value: 30));

        // Act
        var response = httpClientAdapter.GetCancellationTokenForRequest(request: requestMock.Object, cancellationToken: cancellationTokenSource.Token);

        // Assert
        response.Should().NotBeNull();
        response.IsCancellationRequested.Should().BeFalse();
    }
}
