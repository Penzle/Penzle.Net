// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Http.Internal;

public class Connection : IConnection
{
    private readonly Authenticator _authenticator;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly Uri _penzleCloudApi = new("https://api.penzle.com");
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
        Guard.ArgumentNotNull(apiOptions, nameof(apiOptions));
        Guard.ArgumentNotNull(credentialStore, nameof(credentialStore));
        Guard.ArgumentNotNull(httpClient, nameof(httpClient));
        Guard.ArgumentNotNull(serializer, nameof(serializer));
        Guard.ArgumentNotNull(platformInformation, nameof(platformInformation));

        _authenticator = new Authenticator(credentialStore);
        _jsonSerializer = serializer;
        _platformInformation = platformInformation;
        HttpClient = httpClient;
        UserAgent = FormatUserAgent(new ProductHeaderValue("Penzle.Core.Net"));
        Credentials = credentialStore.GetCredentials().GetAwaiter().GetResult();
        CredentialStore = credentialStore;
        SetBaseAddress(baseAddress, apiOptions);
    }


    public virtual IHttpClient HttpClient { get; set; }
    public virtual string UserAgent { get; set; }
    public virtual ICredentialStore<BearerCredentials> CredentialStore { get; set; }

    public virtual async Task<T> Get<T>(
        Uri uri,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));
        return await SendEntry<T>(uri.ApplyParameters(parameters), HttpMethod.Get, null, accepts, contentType,
            cancellationToken);
    }

    public virtual async ValueTask<HttpStatusCode> Patch(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        await SendEntry<object>(uri.ApplyParameters(parameters), HttpMethod.Patch, body, accepts, contentType,
            cancellationToken);
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
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        return SendEntry<T>(uri.ApplyParameters(parameters), HttpMethod.Patch, body, accepts, contentType,
            cancellationToken);
    }

    public virtual async ValueTask<HttpStatusCode> Post(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        await SendEntry<object>(uri.ApplyParameters(parameters), HttpMethod.Post, body, accepts, contentType,
            cancellationToken);
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
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        return SendEntry<T>(uri.ApplyParameters(parameters), HttpMethod.Post, body, accepts, contentType,
            cancellationToken);
    }

    public virtual async ValueTask<HttpStatusCode> Put(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        await SendEntry<object>(uri.ApplyParameters(parameters), HttpMethod.Put, body, accepts, contentType,
            cancellationToken);
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
        Guard.ArgumentNotNull(uri, nameof(uri));
        Guard.ArgumentNotNull(uri, nameof(body));

        return SendEntry<T>(uri.ApplyParameters(parameters), HttpMethod.Put, body, accepts, contentType,
            cancellationToken);
    }

    public virtual async ValueTask<HttpStatusCode> Delete(
        Uri uri,
        object body,
        IDictionary<string, string> parameters,
        string accepts,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));

        await SendEntry<object>(uri.ApplyParameters(parameters), HttpMethod.Delete, body, accepts, contentType,
            cancellationToken);
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
        Guard.ArgumentNotNull(uri, nameof(uri));
        return SendEntry<T>(uri.ApplyParameters(parameters), HttpMethod.Delete, body, accepts, contentType,
            cancellationToken);
    }

    public virtual Uri BaseAddress { get; set; }
    public virtual Credentials Credentials { get; set; }

    public virtual void SetRequestTimeout(TimeSpan timeout)
    {
        HttpClient.SetRequestTimeout(timeout);
    }

    private void SetBaseAddress(Uri baseAddress, ApiOptions apiOptions)
    {
        if (baseAddress == null)
        {
            BaseAddress = Constants.AddressTemplate.FormatUri(_penzleCloudApi, apiOptions.Project, apiOptions.Environment);
            return;
        }

        if (!baseAddress.IsAbsoluteUri)
        {
            throw new ArgumentException(
                string.Format(CultureInfo.InvariantCulture, "The base address '{0}' must be an absolute URI",
                    baseAddress), nameof(baseAddress));
        }

        if (baseAddress.ToString().EndsWith("/"))
        {
            baseAddress = new Uri(baseAddress.ToString().Remove(baseAddress.ToString().Length - 1));
        }

        BaseAddress = Constants.AddressTemplate.FormatUri(baseAddress, apiOptions.Project, apiOptions.Environment);
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
        Guard.ArgumentNotNull(uri, nameof(uri));
        BaseAddress = baseAddress ?? BaseAddress;
        var ignoreProject = false;
        if (uri.ToString().StartsWith(Constants.IgnoreProjectKeyword))
        {
            ignoreProject = true;
            uri = uri.ToString().Replace(Constants.IgnoreProjectKeyword, string.Empty).FormatUri();
            BaseAddress = BaseAddress.ReplaceRelativeUri(new Uri(Constants.AddressWithoutProjectTemplate, UriKind.RelativeOrAbsolute));
        }

        var request = new Request { Method = method, BaseAddress = baseAddress ?? BaseAddress, Endpoint = uri };

        return SendEntryInternal<T>(body, cancellationToken, request, ignoreProject, accepts, contentType);
    }

    internal virtual Task<T> SendEntryInternal<T>(
        object body,
        CancellationToken cancellationToken,
        Request request,
        bool ignoreProject = false,
        string accepts = "*/*",
        string contentType = "application/json")
    {
        request.Headers["Accept"] = accepts ?? "*/*";

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
                        request.Body = _jsonSerializer.Serialize(body);
                        request.ContentType = contentType ?? "application/json";
                    }

                    break;
                }
        }

        return Run<T>(request, ignoreProject, cancellationToken);
    }

    internal virtual async Task<T> Run<T>(IRequest request, bool ignoreProject = false, CancellationToken cancellationToken = default)
    {
        var types = new[] { typeof(Template), typeof(Asset), typeof(Guid) };
        T @object = default;

        var response = await RunRequest(request, cancellationToken).ConfigureAwait(false);
        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return @object;
            case HttpStatusCode.NotFound:
                return default;
        }

        if (ignoreProject || typeof(T).IsGenericType || types.Contains(typeof(T)))
        {
            return _jsonSerializer.Deserialize<T>(response.Body.ToString());
        }

        var jsonNode = JsonNode.Parse(response.Body.ToString())?.AsObject() ?? new JsonObject();

        if (jsonNode.ContainsKey("fields"))
        {
            @object = _jsonSerializer.Deserialize<T>(jsonNode["fields"]?.ToJsonString());
        }

        @object = SetProperty(jsonNode, @object, "system", info => info.PropertyType.BaseType == typeof(BaseSystem));
        @object = SetProperty(jsonNode, @object, "base", info => info.PropertyType == typeof(List<BaseTemplates>));

        return @object;
    }

    internal virtual T SetProperty<T>(
        JsonObject jsonNode,
        T @object,
        string propertyType,
        Func<PropertyInfo, bool> predicate)
    {
        var systemPropertyInfo = @object != null ? @object.GetProperties().FirstOrDefault(predicate) : null;
        if (systemPropertyInfo == null || !jsonNode.ContainsKey(propertyType))
        {
            return @object;
        }

        var system =
            _jsonSerializer.Deserialize(jsonNode[propertyType].ToJsonString(), systemPropertyInfo.PropertyType);
        @object.SetPropertyValue(systemPropertyInfo.Name, system);

        return @object;
    }

    internal virtual async Task<IResponse> RunRequest(IRequest request, CancellationToken cancellationToken)
    {
        request.Headers.Add("User-Agent", UserAgent);
        await _authenticator.Apply(request).ConfigureAwait(false);
        var response = await HttpClient.Send(request, cancellationToken).ConfigureAwait(false);
        HandleErrors(response);
        return response;
    }

    internal virtual void HandleErrors(IResponse response)
    {
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new BadRequestException(response);
        }

        if ((int)response.StatusCode >= 500)
        {
            throw new PenzleException(response);
        }
    }

    internal virtual string FormatUserAgent(ProductHeaderValue productInformation)
    {
        return string.Format(CultureInfo.InvariantCulture, "{0} ({1}; {2}; penzle.net {3})",
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
