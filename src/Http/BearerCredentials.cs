using Penzle.Core.Authentication;

namespace Penzle.Core.Http;

public sealed class BearerCredentials : Credentials
{
    public BearerCredentials(string apiDeliveryKey, string apiManagementKey) : base(authenticationType: AuthenticationType.Bearer)
    {
        ApiDeliveryKey = apiDeliveryKey;
        ApiManagementKey = apiManagementKey;
    }

    public string ApiDeliveryKey { get; }

    public string ApiManagementKey { get; }
}