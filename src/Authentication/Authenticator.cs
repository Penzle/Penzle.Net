using Penzle.Core.Http;
using Penzle.Core.Utilities;

namespace Penzle.Core.Authentication;

internal sealed class Authenticator
{
    private readonly Dictionary<AuthenticationType, IAuthenticationHandler> _authenticators =
        new()
        {
            { AuthenticationType.Bearer, new BearerTokenAuthenticator() }
        };

    public Authenticator(ICredentialStore<BearerCredentials> credentialStore)
    {
        Guard.ArgumentNotNull(value: credentialStore, name: nameof(credentialStore));
        CredentialStore = credentialStore;
    }

    public ICredentialStore<BearerCredentials> CredentialStore { get; set; }

    public async Task Apply(IRequest request)
    {
        Guard.ArgumentNotNull(value: request, name: nameof(request));
        var credentials = await CredentialStore.GetCredentials().ConfigureAwait(continueOnCapturedContext: false) ?? throw new Exception(message: "Cannot get the credentials from credential store.");
        _authenticators[key: credentials.AuthenticationType].Authenticate(request: request, credentials: credentials);
    }
}