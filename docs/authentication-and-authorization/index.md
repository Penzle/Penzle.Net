### **Introduction**

To establish a connection with the Penzle Backend System using the SDK, the client must be aware of the location of the particular API, or more specially, the API URL.

API URLs are standardized and stick to a common format. The structure of URL:

```
https://api-{username-or-tenant-name}.penzle.com/api/
```

Where your username is the tenant name or your regular **penzle username** that you set up when creating your account. To identify a customer in a Penzle system we are providing a unique username. This ensures that all customer data is isolated and secured.

### **Authentication**

Penzle uses the API keys connected to your account to verify your API requests. The Penzle backend API will throw an exception like "unauthorized" if you don't supply a key, and it will produce an authentication error if the key is either invalid or out of date. You can modify the settings for a specific project and environment using the admin panel's setting section navigation, and you can also create secret API keys there.

#### Recommendation

Protect your API keys, as they have the potential to grant system access to multiple users. Do not expose your private API keys in publicly accessible locations, such as GitHub, GitLub, client-side code, or any other similar location.

The client must pass a legitimate token for the connection to be established (key). We created delivery and management clients because we began with a secure architecture, which means you must provide different keys depending on which client you are using.

Simple illustration of pulling keys from configuration or using an in-memory approach. (It is not advised to use in memory approuch.)

```csharp

var credentialStore = new InMemoryCredentialStore
(
    new BearerCredentials
    (
        apiDeliveryKey: "b1afNK_gho6H8SqZ9l0UuIWfQ4-4E1PaD2y8z4LOILc...",
        apiManagementKey:  "b1afNK_gho6H8SqZ9l0UuIWfQ4-4E1PaD2y8z4LOILc..."
    )
);
```

Than pass the `credentialStore` to connection API.

```csharp

...

IConnection connection = new Connection
(
    baseAddress: baseAddress,
    apiOptions: apiOptions,
    credentialStore: credentialStore,
    httpClient: new HttpClientAdapter(() => new HttpClientHandler()),
    serializer: new MicrosoftJsonSerializer()
);

_client = new DeliveryPenzleClient(connection);
```

As part of our .NET SDK, we are giving the flexibility to choose your credential store to developers. However, we strongly recommend storing your credentials outside of the application itself. We are making it possible for you to store your credentials for the distribution and administration of content in a manner that is more safe by making use of services like Azure Key Vault and other similar services in cloud. This can be accomplished with little effort by implementing the `ICredentialStore` contract, and there is a straightforward example available for [Azure Key Vault](https://azure.microsoft.com/en-us/products/key-vault).


```csharp
/// <summary>
///     Abstraction that allows for the interaction of credentials
/// </summary>
public interface ICredentialStore<T>
{
	/// <summary>
	///     Find the credentials in the underlying store and retrieve them.
	/// </summary>
	/// <returns>A continuation that includes credential information</returns>
	Task<T> GetCredentials();
}
```

The below example of how to implement Azure Key Value shows how to this.

```csharp
public sealed class KeyVaultCredentialStore : ICredentialStore<BearerCredentials>
{
    const string uri = $ "https://{keyVaultName}.vault.azure.net";
    SecretClient client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

    public Task<BearerCredentials> GetCredentials()
    {
        var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
        var secretValue = Environment.GetEnvironmentVariable("SECRET_VALUE");

        var apiDeliveryKey = await client.GetSecretAsync("apiDeliveryKey");
        var apiManagementKey = await client.GetSecretAsync("apiManagementKey");

        return new BearerCredentials(apiDeliveryKey, apiManagementKey);
    }
}
```

### **Authorization**

The authorization has been done out in such a manner that you are able to offer special permission to your keys, such as for a project or a particular environment. For instance, if you are building the project, you need to have at least two environments, with one of those environments serving as the development environment. The keys for the development environment are only valid for testing reasons. **They cannot be used in production.**

Any API call, including delivery and management of any content, can be made on your behalf using your account's private API key, which anyone can use. In order to be safe please please follow the next safety rules:

- Just those individuals who require access should be given it.
- As described above, ensure that the key is excluded from any version control system you may use.
- Please do not include your keys in your mobile application, as they are easily extracted.

> ⚠️ Development and testing should only use test API keys. This prevents accidental changes to live clients.
