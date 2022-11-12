namespace Penzle.Core;

/// <inheritdoc>
///     <cref>IDeliveryPenzleClient</cref>
/// </inheritdoc>
public sealed class DeliveryPenzleClient : IDeliveryPenzleClient
{
    public DeliveryPenzleClient(IConnection connection)
    {
        Guard.ArgumentNotNull(value: connection, name: nameof(connection));

        var apiConnection = new ApiConnection(connection: connection);
        Form = new RestFormClient(apiConnection: apiConnection);
        Entry = new RestEntryClient(apiConnection: apiConnection);
        Template = new RestTemplateClient(apiConnection: apiConnection);
        Asset = new RestAssetClient(apiConnection: apiConnection);
    }

    /// <inheritdoc cref="Entry{TEntity}" />
    public IDeliveryEntryClient Entry { get; }

    /// <inheritdoc cref="Form{TEntity}" />
    public IDeliveryFormClient Form { get; }

    /// <inheritdoc cref="Models.Template" />
    public IDeliveryTemplateClient Template { get; }

    /// <inheritdoc cref="Models.Asset" />
    public IDeliveryAssetClient Asset { get; }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <returns>The default instance which is matched <see cref="IDeliveryPenzleClient" />IPenzleClient</returns>
    public static IDeliveryPenzleClient Factory(Uri baseAddress, string apiDeliveryKey)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiOptions: options =>
            {
                options.Environment = ApiOptions.Default.Environment;
                options.Project = ApiOptions.Default.Project;
            },
            httpClient: new HttpClientAdapter(getHandler: () => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <returns>The default instance which is matched <see cref="IDeliveryPenzleClient" />IPenzleClient</returns>
    public static IDeliveryPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, Action<ApiOptions> apiOptions)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiOptions: apiOptions,
            httpClient: new HttpClientAdapter(getHandler: () => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <returns>The default instance which is matched <see cref="IDeliveryPenzleClient" />IPenzleClient</returns>
    public static IDeliveryPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, Action<ApiOptions> apiOptions, IHttpClient httpClient)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(value: 30)
        );
    }


    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <returns>The default instance which is matched <see cref="IDeliveryPenzleClient" />IPenzleClient</returns>
    public static IDeliveryPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiDeliveryKey: apiDeliveryKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: jsonSerializer,
            timeOut: FromSeconds(value: 30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiDeliveryKey">The key for delivery.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <param name="timeOut">Pass custom time for cancel request on global level.</param>
    /// <returns>The default instance which is matched <see cref="IDeliveryPenzleClient" />IPenzleClient</returns>
    public static IDeliveryPenzleClient Factory(Uri baseAddress, string apiDeliveryKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer, TimeSpan timeOut)
    {
        ICredentialStore<BearerCredentials> credentialStore = new InMemoryCredentialStore(credentials: new BearerCredentials(apiDeliveryKey: apiDeliveryKey, apiManagementKey: null));

        var options = ApiOptions.Default;
        apiOptions?.Invoke(obj: options);

        IConnection connection = new Connection(baseAddress: baseAddress, apiOptions: options, credentialStore: credentialStore, httpClient: httpClient, serializer: jsonSerializer, platformInformation: new SdkPlatformInformation());
        connection.SetRequestTimeout(timeout: timeOut);

        return new DeliveryPenzleClient(connection: connection);
    }
}
