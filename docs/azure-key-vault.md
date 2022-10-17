## **Make sure your keys are safe.**

Through the use of services such as Azure Key Vault, we are providing a more secure method for you to store your
credentials for the delivery and management of content. `PenzleClient` can be configured to retrieve either protected or
unpublished content from the following types of service-implemented interfaces:

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

The example that is represented below is one of the ways that such an ability can be implemented:

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
