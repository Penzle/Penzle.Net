namespace Penzle.Core.Http.Internal;

public class Connection : IConnection
{
    private readonly Uri _penzleCloudApi = new Uri(uriString: "https://api.penzle.com");
    private readonly Authenticator _authenticator;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IPlatformInformation _platformInformation;

    public Connection()
    {
        _jsonSerializer = new MicrosoftJsonSerializer();
        _platformInformation = new SdkPlatformInformation();
    }

    public Connection(
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClient,
        IJsonSerializer serializer,
        IPlatformInformation platformInformation,
        Uri baseAddress = null)
    {
        Guard.ArgumentNotNull(value: apiOptions, name: nameof(apiOptions));
        Guard.ArgumentNotNull(value: credentialStore, name: nameof(credentialStore));
        Guard.ArgumentNotNull(value: httpClient, name: nameof(httpClient));
        Guard.ArgumentNotNull(value: serializer, name: nameof(serializer));
        Guard.ArgumentNotNull(value: platformInformation, name: nameof(platformInformation));

        _authenticator = new Authenticator(credentialStore: credentialStore);
        _jsonSerializer = serializer;
        _platformInformation = platformInformation;
        HttpClient = httpClient;
        UserAgent = FormatUserAgent(productInformation: new ProductHeaderValue(name: "Penzle.Core.Net"));
        Credentials = credentialStore.GetCredentials().GetAwaiter().GetResult();
        CredentialStore = credentialStore;
        SetBaseAdress(baseAddress, apiOptions);
    }

    
    public virtual IHttpClient HttpClient { get; set; }
    public virtual string UserAgent { get; set; }
    public virtual ICredentialStore<BearerCredentials> CredentialStore { get; set; }

    private void SetBaseAdress(Uri? baseAddress, ApiOptions apiOptions)
    {
        if (baseAddress == null)
        {
            BaseAddress = Constants.AddressTemplate.FormatUri(_penzleCloudApi, apiOptions.Project, apiOptions.Environment);
            return;
        }
        
        if (!baseAddress.IsAbsoluteUri)
        {
            throw new ArgumentException(
                message: string.Format(provider: CultureInfo.InvariantCulture, format: "The base address '{0}' must be an absolute URI",
                    arg0: baseAddress), paramName: nameof(baseAddress));
        }

        if (baseAddress.ToString().EndsWith(value: "/"))
        {
            baseAddress = new Uri(uriString: baseAddress.ToString().Remove(startIndex: baseAddress.ToString().Length - 1));
        }

        BaseAddress = Constants.AddressTemplate.FormatUri(baseAddress, apiOptions.Project, apiOptions.Environment);
    }

    public async virtual Task<T> Get<T>(
        Uri uri,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Get, body: null, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
    }

    public async virtual ValueTask<HttpStatusCode> Patch(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Patch, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public virtual Task<T> Patch<T>(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Patch, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
    }

    public async virtual ValueTask<HttpStatusCode> Post(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Post, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public virtual Task<T> Post<T>(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Post, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
    }

    public async virtual ValueTask<HttpStatusCode> Put(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Put, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public virtual Task<T> Put<T>(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Put, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
    }

    public async virtual ValueTask<HttpStatusCode> Delete(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));

        await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Delete, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public virtual Task<T> Delete<T>(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Delete, body: body, accepts: accepts, contentType: contentType,
            cancellationToken: cancellationToken);
    }

    public virtual Uri BaseAddress { get; set; }
    public virtual Credentials Credentials { get; set; }

    public virtual void SetRequestTimeout(TimeSpan timeout)
    {
        HttpClient.SetRequestTimeout(timeout: timeout);
    }

    internal virtual Task<T> SendEntry<T>(
        Uri uri,
        HttpMethod method,
        object body,
        string accepts,
        string contentType,
        CancellationToken cancellationToken,
        Uri baseAddress = null)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        var request = new Request
        {
            Method = method, BaseAddress = baseAddress ?? BaseAddress, Endpoint = uri
        };

        return SendEntryInternal<T>(body: body, cancellationToken: cancellationToken, request: request, accepts: accepts, contentType: contentType);
    }

    internal virtual Task<T> SendEntryInternal<T>(
        object body,
        CancellationToken cancellationToken,
        Request request,
        string accepts = "*/*",
        string contentType = "application/json")
    {
        request.Headers[key: "Accept"] = accepts ?? "*/*";

        switch (body)
        {
            case MultipartFormDataContent:
                request.Body = body;
                request.ContentType = contentType;
                break;
            default:
                {
                    if (body != null)
                    {
                        request.Body = _jsonSerializer.Serialize(item: body);
                        request.ContentType = contentType ?? "application/json";
                    }

                    break;
                }
        }

        return Run<T>(request: request, cancellationToken: cancellationToken);
    }

    async internal virtual Task<T> Run<T>(IRequest request, CancellationToken cancellationToken)
    {
        var types = new[]
        {
            typeof(Template), typeof(Asset), typeof(Guid)
        };
        T @object = default;

        var response = await RunRequest(request: request, cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return @object;
            case HttpStatusCode.NotFound:
                return default;
        }

        if (typeof(T).IsGenericType || types.Contains(value: typeof(T)))
        {
            return _jsonSerializer.Deserialize<T>(json: response.Body.ToString());
        }

        var jsonNode = JsonNode.Parse(json: response.Body.ToString())?.AsObject() ?? new JsonObject();

        if (jsonNode.ContainsKey(propertyName: "fields"))
        {
            @object = _jsonSerializer.Deserialize<T>(json: jsonNode[propertyName: "fields"]?.ToJsonString());
        }

        @object = SetProperty(jsonNode: jsonNode, @object: @object, propertyType: "system", predicate: info => info.PropertyType.BaseType == typeof(BaseSystem));
        @object = SetProperty(jsonNode: jsonNode, @object: @object, propertyType: "base", predicate: info => info.PropertyType == typeof(List<BaseTemplates>));

        return @object;
    }

    internal virtual T SetProperty<T>(
        JsonObject jsonNode,
        T @object,
        string propertyType,
        Func<PropertyInfo, bool> predicate)
    {
        var systemPropertyInfo = @object != null ? @object.GetProperties().FirstOrDefault(predicate: predicate) : null;
        if (systemPropertyInfo == null || !jsonNode.ContainsKey(propertyName: propertyType))
        {
            return @object;
        }

        var system =
            _jsonSerializer.Deserialize(json: jsonNode[propertyName: propertyType].ToJsonString(), returnType: systemPropertyInfo.PropertyType);
        @object.SetPropertyValue(propertyName: systemPropertyInfo.Name, value: system);

        return @object;
    }

    async internal virtual Task<IResponse> RunRequest(IRequest request, CancellationToken cancellationToken)
    {
        request.Headers.Add(key: "User-Agent", value: UserAgent);
        await _authenticator.Apply(request: request).ConfigureAwait(continueOnCapturedContext: false);
        var response = await HttpClient.Send(request: request, cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        HandleErrors(response: response);
        return response;
    }

    internal virtual void HandleErrors(IResponse response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        if ((int)response.StatusCode >= 400)
        {
            throw new PenzleException(response: response);
        }
    }

    internal virtual string FormatUserAgent(ProductHeaderValue productInformation)
    {
        return string.Format(provider: CultureInfo.InvariantCulture, format: "{0} ({1}; {2}; penzle.net {3})",
            productInformation,
            _platformInformation.GetPlatformInformation(),
            GetCultureInformation(),
            GetVersionInformation());
    }

    internal virtual string GetCultureInformation()
    {
        return CultureInfo.CurrentCulture.Name;
    }

    internal virtual string GetVersionInformation()
    {
        return Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0";
    }
}
