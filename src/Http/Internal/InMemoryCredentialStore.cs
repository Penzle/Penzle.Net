using Penzle.Core.Utilities;

namespace Penzle.Core.Http.Internal;

public sealed class InMemoryCredentialStore : ICredentialStore<BearerCredentials>
{
    private readonly BearerCredentials _credentials;

    public InMemoryCredentialStore(BearerCredentials credentials)
    {
        Guard.ArgumentNotNull(value: credentials, name: nameof(credentials));

        _credentials = credentials;
    }

    public Task<BearerCredentials> GetCredentials()
    {
        return Task.FromResult(result: _credentials);
    }
}