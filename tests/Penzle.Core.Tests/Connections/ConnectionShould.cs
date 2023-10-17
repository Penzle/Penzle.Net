// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Moq.Protected;

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

    [Theory]
    [ConnectionDependenciesData]
    public void Ability_To_Set_Request_TimeOut(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);
        var timeOut = TimeSpan.FromMicroseconds(10);

        var mockResponse = new Mock<IResponse>();
        mockResponse
            .SetupAllProperties()
            .SetupProperty(property: response => response.StatusCode, initialValue: HttpStatusCode.NotFound);

        // Act
        connection.SetRequestTimeout(timeOut);

        // Assert
        connection.HttpClient.HttpClient.Timeout.Should().Be(timeOut);
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Generic_Get_Http_Request(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync(path: "Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);

        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(payload)
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Get<Entry<Article>>(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Fields.Should().BeAssignableTo<Article>();
        response.System.Should().NotBeNull();
        response.Base.Should().NotBeNullOrEmpty();

        response.Fields.Banner.Should().Be("e879281a-a225-4f82-a4af-740884dcee1b");
        response.Fields.Title.Should().Be("Getting started with Penzle");
        response.Fields.Slug.Should().Be("getting-started-with-penzle");
        response.Fields.Description.Should().Be("With the help of Penzle's headless content platform, developers can use the toolkits and languages most appropriate for the project.");
        response.Fields.Body.Should().NotBeNullOrWhiteSpace();
        response.Fields.RelatedArticle.Should().Be("c442e7ef-155e-4c28-8fe6-c094d07946ea");

        response.System.Id.Should().Be("4d40413f-587c-442b-949e-c44771096ee3");
        response.System.Template.Should().BeLowerCased(nameof(Article));
        response.System.ParentId.Should().Be("16ea7830-fc9c-42f8-9b19-fe9d9c3d31bd");
        response.System.Name.Should().Be("Getting started with Penzle");
        response.System.Version.Should().Be("24.0");
        response.System.Language.Should().Be("en-US");
        response.System.Slug.Should().Be("getting-started-with-penzle");
        response.System.ModifiedAt.Should().Be(new DateTime(year: 2022, month: 11, day: 08, hour: 07, minute: 53, second: 09, kind: DateTimeKind.Utc));
        response.System.CreatedAt.Should().Be(new DateTime(year: 2022, month: 10, day: 25, hour: 08, minute: 48, second: 10, kind: DateTimeKind.Utc));
    }


    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Post(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content: "\"B9FCA947-1313-4AAF-8043-7F8323F280E4\"", encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Post<Guid>(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Post, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeEmpty();
        response.Should().Be("B9FCA947-1313-4AAF-8043-7F8323F280E4");
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Post_Non_Generic(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = null
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Post(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Post, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Put_Non_Generic(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntries.json").ConfigureAwait(false);

        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Put(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Put, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Put(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content: "\"B9FCA947-1313-4AAF-8043-7F8323F280E4\"", encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Put<Guid>(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Put, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeEmpty();
        response.Should().Be("B9FCA947-1313-4AAF-8043-7F8323F280E4");
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Patch(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content: "\"B9FCA947-1313-4AAF-8043-7F8323F280E4\"", encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Patch<Guid>(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Patch, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeEmpty();
        response.Should().Be("B9FCA947-1313-4AAF-8043-7F8323F280E4");
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Patch_Non_Generic(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntries.json").ConfigureAwait(false);

        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Patch(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Patch, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Delete(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content: "\"B9FCA947-1313-4AAF-8043-7F8323F280E4\"", encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Delete<Guid>(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Delete, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeEmpty();
        response.Should().Be("B9FCA947-1313-4AAF-8043-7F8323F280E4");
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Ability_To_Send_Http_Request_Using_Delete_Non_Generic(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntries.json").ConfigureAwait(false);

        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json")
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        // Act
        var response = await connection.Delete(uri: new Uri(uriString: "/api/project/main/environment/development/entries/4d40413f-587c-442b-949e-c44771096ee3", uriKind: UriKind.Relative), body: HttpMethod.Delete, parameters: new Dictionary<string, string>(), accepts: "text/json", contentType: "text/json", cancellationToken: CancellationToken.None);

        // Assert
        response.Should().Be(HttpStatusCode.NoContent);
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Processing_Request_Internally_Have_To_Break_On_MultipartFormDataContent(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);

        var content = new MultipartFormDataContent
        {
            new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json")
        };

        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);
        var request = new Request
        {
            Body = content,
            Method = HttpMethod.Post,
            BaseAddress = new Uri(uriString: "https://api.penzle.com", uriKind: UriKind.Absolute),
            Endpoint = new Uri(uriString: "/api/project/main/environment/development/entries", uriKind: UriKind.Relative),
            ContentType = "multipart/form-data"
        };

        // Act
        var handler = () => connection.SendEntryInternal<Article>(body: content, cancellationToken: CancellationToken.None, request: request);

        // Assert 
        await handler.Should().ThrowAsync<Exception>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Processing_Run_Request_Have_To_Populate_Strong_Type_Object(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);
        var content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json");
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var request = new Request
        {
            Body = content,
            Method = HttpMethod.Post,
            BaseAddress = new Uri(uriString: "https://api.penzle.com", uriKind: UriKind.Absolute),
            Endpoint = new Uri(uriString: "/api/project/main/environment/development/entries", uriKind: UriKind.Relative),
            ContentType = "text/json"
        };

        // Act
        var response = await connection.Run<Article>(request: request, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<Article>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Processing_Run_Request_Have_To_Populate_Strong_Type_Object_With_System_Properties(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);
        var content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json");
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var request = new Request
        {
            Body = content,
            Method = HttpMethod.Post,
            BaseAddress = new Uri(uriString: "https://api.penzle.com", uriKind: UriKind.Absolute),
            Endpoint = new Uri(uriString: "/api/project/main/environment/development/entries", uriKind: UriKind.Relative),
            ContentType = "text/json"
        };

        // Act
        var response = await connection.Run<ArticleWithSystem>(request: request, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<ArticleWithSystem>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Processing_Run_Request_Have_To_Populate_Entry_Strong_Type_Object(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);
        var content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json");
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var request = new Request
        {
            Body = content,
            Method = HttpMethod.Post,
            BaseAddress = new Uri(uriString: "https://api.penzle.com", uriKind: UriKind.Absolute),
            Endpoint = new Uri(uriString: "/api/project/main/environment/development/entries", uriKind: UriKind.Relative),
            ContentType = "text/json"
        };

        // Act
        var response = await connection.Run<Entry<Article>>(request: request, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<Entry<Article>>();
    }

    [Theory]
    [ConnectionDependenciesData]
    public async Task Processing_Run_Request_Have_To_Populate_Well_Know_Type_Object(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClientAdapter,
        IJsonSerializer jsonSerializer,
        IPlatformInformation platformInformation)
    {
        // Arrange
        var payload = await File.ReadAllTextAsync("./Fixtures/Payloads/GetContentEntry.json").ConfigureAwait(false);
        var content = new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "text/json");
        var mockHttp = new Mock<HttpMessageHandler>();
        mockHttp
            .Protected()
            .Setup<Task<HttpResponseMessage>>(methodOrPropertyName: "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = content
            });

        httpClientAdapter = new HttpClientAdapter(() => mockHttp.Object);
        var connection = new Connection(baseAddress: baseAddress, apiOptions: apiOptions, credentialStore: credentialStore, httpClient: httpClientAdapter, serializer: jsonSerializer, platformInformation: platformInformation);

        var request = new Request
        {
            Body = content,
            Method = HttpMethod.Post,
            BaseAddress = new Uri(uriString: "https://api.penzle.com", uriKind: UriKind.Absolute),
            Endpoint = new Uri(uriString: "/api/project/main/environment/development/entries", uriKind: UriKind.Relative),
            ContentType = "text/json"
        };

        // Act
        var response = await connection.Run<Template>(request: request, cancellationToken: CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<Template>();
    }
}
