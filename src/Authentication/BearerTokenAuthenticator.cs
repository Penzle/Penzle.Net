namespace Penzle.Core.Authentication;

internal sealed class BearerTokenAuthenticator : IAuthenticationHandler
{
    public void Authenticate(IRequest request, Credentials credentials)
    {
        Guard.ArgumentNotNull(value: request, name: nameof(request));
        Guard.ArgumentNotNull(value: credentials, name: nameof(credentials));

        if (credentials is not BearerCredentials bearerCredentials)
        {
            return;
        }

        request.Headers[key: "Authorization"] = string.Format(provider: CultureInfo.InvariantCulture, format: "Bearer {0}", arg0: bearerCredentials.ApiManagementKey ?? bearerCredentials.ApiDeliveryKey);
    }
}
