namespace Penzle.Core;

/// <inheritdoc>
///     <cref>IManagementPenzleClient</cref>
/// </inheritdoc>
public sealed class ManagementPenzleClient : IManagementPenzleClient
{
    public ManagementPenzleClient(IConnection connection)
    {
        Guard.ArgumentNotNull(connection, nameof(connection));

        var apiConnection = new ApiConnection(connection);
        Form = new RestFormClient(apiConnection);
        Entry = new RestEntryClient(apiConnection);
        Asset = new RestAssetClient(apiConnection);
        User = new RestUserClient(apiConnection);
    }

    /// <inheritdoc cref="Entry{TEntity}" />
    public IManagementEntryClient Entry { get; }

    /// <inheritdoc cref="Form{TEntity}" />
    public IManagementFormClient Form { get; }

    /// <inheritdoc cref="Models.Asset" />
    public IManagementAssetClient Asset { get; }

    /// <inheritdoc />
    public IManagementUserClient User { get; }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(string apiManagementKey, Uri baseAddress = null)
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
            httpClient: new HttpClientAdapter(() => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(string apiManagementKey, Action<ApiOptions> apiOptions, Uri baseAddress = null)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: new HttpClientAdapter(() => new HttpClientHandler()),
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, Uri baseAddress = null)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: new MicrosoftJsonSerializer(),
            timeOut: FromSeconds(30)
        );
    }


    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer,
        Uri baseAddress = null)
    {
        return Factory
        (
            baseAddress: baseAddress,
            apiManagementKey: apiManagementKey,
            apiOptions: apiOptions,
            httpClient: httpClient,
            jsonSerializer: jsonSerializer,
            timeOut: FromSeconds(30)
        );
    }

    /// <summary>
    ///     Create instance of Penzle Client Management API.
    /// </summary>
    /// <param name="apiManagementKey">The key for management.</param>
    /// <param name="apiOptions">Pass additional options as project id and environments id.</param>
    /// <param name="httpClient">Pass your own implementation of http client.</param>
    /// <param name="jsonSerializer">
    ///     Pass your own implementation of json serializer. The current implementation coming from
    ///     System.Text.Json
    /// </param>
    /// <param name="timeOut">Pass custom time for cancel request on global level.</param>
    /// <param name="baseAddress">The base address of api endpoint.</param>
    /// <returns>The default instance which is matched <see cref="IManagementPenzleClient" />IPenzleClient</returns>
    public static IManagementPenzleClient Factory(string apiManagementKey, Action<ApiOptions> apiOptions, IHttpClient httpClient, IJsonSerializer jsonSerializer, TimeSpan timeOut,
        Uri baseAddress)
    {
        ICredentialStore<BearerCredentials> credentialStore = new InMemoryCredentialStore(new BearerCredentials(null, apiManagementKey));

        var options = ApiOptions.Default;
        apiOptions?.Invoke(options);

        IConnection connection = new Connection(baseAddress: baseAddress, apiOptions: options, credentialStore: credentialStore, httpClient: httpClient, serializer: jsonSerializer,
            platformInformation: new SdkPlatformInformation());
        connection.SetRequestTimeout(timeOut);

        return new ManagementPenzleClient(connection);
    }
}
