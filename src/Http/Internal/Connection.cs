using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Authentication;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;
using Penzle.Core.Utilities;

namespace Penzle.Core.Http.Internal;

public sealed class Connection : IConnection
{
    private static string _platformInformation;

    private readonly Authenticator _authenticator;
    private readonly IHttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public Connection(
        Uri baseAddress,
        ApiOptions apiOptions,
        ICredentialStore<BearerCredentials> credentialStore,
        IHttpClient httpClient,
        IJsonSerializer serializer)
    {
        Guard.ArgumentNotNull(value: baseAddress, name: nameof(baseAddress));
        Guard.ArgumentNotNull(value: apiOptions, name: nameof(apiOptions));
        Guard.ArgumentNotNull(value: credentialStore, name: nameof(credentialStore));
        Guard.ArgumentNotNull(value: httpClient, name: nameof(httpClient));
        Guard.ArgumentNotNull(value: serializer, name: nameof(serializer));

        if (!baseAddress.IsAbsoluteUri)
        {
            throw new ArgumentException(message: string.Format(provider: CultureInfo.InvariantCulture, format: "The base address '{0}' must be an absolute URI", arg0: baseAddress), paramName: nameof(baseAddress));
        }

        if (baseAddress.ToString().EndsWith(value: "/"))
        {
            baseAddress = new Uri(uriString: baseAddress.ToString().Remove(startIndex: baseAddress.ToString().Length - 1));
        }

        UserAgent = FormatUserAgent(productInformation: new ProductHeaderValue(name: "Penzle.Core.Net"));
        BaseAddress = Constants.AddressTemplate.FormatUri(baseAddress, apiOptions.Project, apiOptions.Environment);
        _authenticator = new Authenticator(credentialStore: credentialStore);
        _httpClient = httpClient;
        _jsonSerializer = serializer;
    }

    private string UserAgent { get; }

    public ICredentialStore<BearerCredentials> CredentialStore => _authenticator.CredentialStore;

    public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Get, body: null, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }

    public async ValueTask<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));


        var response = await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Patch, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public Task<T> Patch<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Patch, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }

    public async ValueTask<HttpStatusCode> Post(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));


        var response = await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Patch, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public Task<T> Post<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Post, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }

    public async ValueTask<HttpStatusCode> Put(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));


        var response = await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Put, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public Task<T> Put<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        Guard.ArgumentNotNull(value: uri, name: nameof(body));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Put, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }

    public async ValueTask<HttpStatusCode> Delete(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));

        var response = await SendEntry<object>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Delete, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
        return HttpStatusCode.NoContent;
    }

    public Task<T> Delete<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));

        return SendEntry<T>(uri: uri.ApplyParameters(parameters: parameters), method: HttpMethod.Delete, body: body, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }


    public Uri BaseAddress { get; }

    public Credentials Credentials => CredentialStore.GetCredentials().GetAwaiter().GetResult();

    public void SetRequestTimeout(TimeSpan timeout)
    {
        _httpClient.SetRequestTimeout(timeout: timeout);
    }

    private Task<T> SendEntry<T>(Uri uri, HttpMethod method, object body, string accepts, string contentType, CancellationToken cancellationToken, Uri baseAddress = null)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        var request = new Request
        {
            Method = method,
            BaseAddress = baseAddress ?? BaseAddress,
            Endpoint = uri
        };

        return SendEntryInternal<T>(body: body, cancellationToken: cancellationToken, request: request, accepts: accepts, contentType: contentType);
    }

    private Task<T> SendEntryInternal<T>(object body, CancellationToken cancellationToken, Request request, string accepts = "*/*", string contentType = "application/json")
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

    private async Task<T> Run<T>(IRequest request, CancellationToken cancellationToken)
    {
        var types = new[] { typeof(Template), typeof(Asset), typeof(Guid) };
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

    private T SetProperty<T>(JsonObject jsonNode, T @object, string propertyType, Func<PropertyInfo, bool> predicate)
    {
        var systemPropertyInfo = @object != null ? @object.GetProperties().FirstOrDefault(predicate: predicate) : null;
        if (systemPropertyInfo == null || !jsonNode.ContainsKey(propertyName: propertyType))
        {
            return @object;
        }

        var system = _jsonSerializer.Deserialize(json: jsonNode[propertyName: propertyType].ToJsonString(), returnType: systemPropertyInfo.PropertyType);
        @object.SetPropertyValue(propertyName: systemPropertyInfo.Name, value: system);

        return @object;
    }

    private async Task<IResponse> RunRequest(IRequest request, CancellationToken cancellationToken)
    {
        request.Headers.Add(key: "User-Agent", value: UserAgent);
        await _authenticator.Apply(request: request).ConfigureAwait(continueOnCapturedContext: false);
        var response = await _httpClient.Send(request: request, cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        HandleErrors(response: response);
        return response;
    }

    private static void HandleErrors(IResponse response)
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

    private static string FormatUserAgent(ProductHeaderValue productInformation)
    {
        return string.Format(provider: CultureInfo.InvariantCulture, format: "{0} ({1}; {2}; penzle.net {3})",
            productInformation,
            GetPlatformInformation(),
            GetCultureInformation(),
            GetVersionInformation());
    }

    private static string GetPlatformInformation()
    {
        if (!string.IsNullOrEmpty(value: _platformInformation))
        {
            return _platformInformation;
        }

        try
        {
            _platformInformation = string.Format(provider: CultureInfo.InvariantCulture, format: "{0} {1}; {2}", arg0: Environment.OSVersion.Platform, arg1: Environment.OSVersion.Version.ToString(fieldCount: 3), arg2: Environment.Is64BitOperatingSystem ? "amd64" : "x86");
        }
        catch
        {
            _platformInformation = "Unknown Platform";
        }

        return _platformInformation;
    }

    private static string GetCultureInformation()
    {
        return CultureInfo.CurrentCulture.Name;
    }

    private static string GetVersionInformation()
    {
        return Assembly.GetExecutingAssembly().GetName()?.Version?.ToString() ?? "1.0.0";
    }
}