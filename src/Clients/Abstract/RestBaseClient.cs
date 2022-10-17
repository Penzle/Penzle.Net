using Penzle.Core.Http;
using Penzle.Core.Utilities;

namespace Penzle.Core.Clients.Abstract;

internal abstract class RestBaseClient
{
    protected RestBaseClient(IApiConnection apiConnection)
    {
        Guard.ArgumentNotNull(value: apiConnection, name: nameof(apiConnection));

        Connection = apiConnection.Connection;
    }

    protected IConnection Connection { get; }
}