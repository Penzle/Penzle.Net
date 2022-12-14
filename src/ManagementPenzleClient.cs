namespace Penzle.Core;

/// <inheritdoc>
///     <cref>IManagementPenzleClient</cref>
/// </inheritdoc>
public sealed class ManagementPenzleClient : IManagementPenzleClient
{
    public ManagementPenzleClient(IConnection connection)
    {
        Guard.ArgumentNotNull(value: connection, name: nameof(connection));

        var apiConnection = new ApiConnection(connection: connection);
        Form = new RestFormClient(apiConnection: apiConnection);
        Entry = new RestEntryClient(apiConnection: apiConnection);
        Asset = new RestAssetClient(apiConnection: apiConnection);
    }

    /// <inheritdoc cref="Entry{TEntity}" />
    public IManagementEntryClient Entry { get; }

    /// <inheritdoc cref="Form{TEntity}" />
    public IManagementFormClient Form { get; }

    /// <inheritdoc cref="Models.Asset" />
    public IManagementAssetClient Asset { get; }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(Uri baseAddress, string apiManagementKey)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
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
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(Uri baseAddress, string apiManagementKey, Action<ApiOptions> apiOptions)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
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
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(Uri baseAddress, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
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
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(Uri baseAddress, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
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
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <param name="timeOut">Pass custom time for cancel request on global level.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(Uri baseAddress, string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer, TimeSpan timeOut)
    {
        ICredentialStore<BearerCredentials> credentialStore = new InMemoryCredentialStore(credentials: new BearerCredentials(apiDeliveryKey: null, apiManagementKey: apiManagementKey));

        var options = ApiOptions.Default;
        apiOptions?.Invoke(obj: options);

        IConnection connection = new Connection(baseAddress: baseAddress, apiOptions: options, credentialStore: credentialStore, httpClient: httpClient, serializer: jsonSerializer, platformInformation: new SdkPlatformInformation());
        connection.SetRequestTimeout(timeout: timeOut);

        return new ManagementPenzleClient(connection: connection);
    }
}
