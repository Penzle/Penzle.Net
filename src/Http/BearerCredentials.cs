namespace Penzle.Core.Http;

/// <summary>
///     Bearer authentication (also known as token authentication) is an HTTP authentication system that uses bearer tokens
///     for security.
/// </summary>
public sealed class BearerCredentials : Credentials
{
    public BearerCredentials(string apiDeliveryKey, string apiManagementKey) : base(authenticationType: AuthenticationType.Bearer)
    {
        ApiDeliveryKey = apiDeliveryKey;
        ApiManagementKey = apiManagementKey;
    }

    /// <summary>
    ///     The key for accessing the API, which may be used to supply content, forms, templates, and media assets.
    /// </summary>
    public string ApiDeliveryKey { get; }

    /// <summary>
    ///     The API key that allows you to alter content, forms, templates, and assets by performing actions such as create,
    ///     update, and delete.
    /// </summary>
    public string ApiManagementKey { get; }
}
