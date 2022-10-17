using System;
using System.Net.Http;
using Penzle.Core.Clients;
using Penzle.Core.Clients.Rest;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Models;
using Penzle.Core.Utilities;

namespace Penzle.Core;

/// <inheritdoc cref="IPenzleClient" />
public sealed class PenzleClient : IPenzleClient
{
    private PenzleClient(IConnection connection)
    {
        Guard.ArgumentNotNull(value: connection, name: nameof(connection));

        var apiConnection = new ApiConnection(connection: connection);
        Form = new RestFormClient(apiConnection: apiConnection);
        Entry = new RestEntryClient(apiConnection: apiConnection);
        Template = new RestTemplateClient(apiConnection: apiConnection);
        Asset = new RestAssetClient(apiConnection: apiConnection);
    }

    /// <inheritdoc cref="IPenzleClient.Entry" />
    public IEntryClient Entry { get; }

    /// <inheritdoc cref="IPenzleClient.Form" />
    public IFormClient Form { get; }

    /// <inheritdoc cref="IPenzleClient.Template" />
    public ITemplateClient Template { get; }

    /// <inheritdoc cref="IPenzleClient.Asset" />
    public IAssetClient Asset { get; }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <returns>The default instance which is matched <see cref="IPenzleClient" />IPenzleClient</returns>
    public static IPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, string apiManagementKey)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiManagementKey: apiManagementKey,
            apiOptions: options =>
            {
                options.Environment = ApiOptions.Default.Environment;
                options.Project = ApiOptions.Default.Project;
            },
            httpClient: new HttpClientAdapter(getHandler: () => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: TimeSpan.FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <returns>The default instance which is matched <see cref="IPenzleClient" />IPenzleClient</returns>
    public static IPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, string apiManagementKey, Action<ApiOptions> apiOptions)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: new HttpClientAdapter(getHandler: () => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: TimeSpan.FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <returns>The default instance which is matched <see cref="IPenzleClient" />IPenzleClient</returns>
    public static IPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: TimeSpan.FromSeconds(value: 30)
        );
    }


    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <returns>The default instance which is matched <see cref="IPenzleClient" />IPenzleClient</returns>
    public static IPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: jsonSerializer,
            timeOut: TimeSpan.FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <param name="timeOut">Pass custom time for cancel request on global level.</param>
    /// <returns>The default instance which is matched <see cref="IPenzleClient" />IPenzleClient</returns>
    public static IPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer, TimeSpan timeOut)
    {
        ICredentialStore<BearerCredentials> credentialStore = new InMemoryCredentialStore(credentials: new BearerCredentials(apiDeliveryKey: apiDeliveryKey, apiManagementKey: apiManagementKey));

        var options = ApiOptions.Default;
        apiOptions?.Invoke(obj: options);

        IConnection connection = new Connection(baseAddress: baseAddress, apiOptions: options, credentialStore: credentialStore, httpClient: httpClient, serializer: jsonSerializer);
        connection.SetRequestTimeout(timeout: timeOut);

        return new PenzleClient(connection: connection);
    }
}