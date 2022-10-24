using System.Net;
using Penzle.Core.Utilities;

namespace Penzle.Core.Http.Internal;

internal class ApiConnection : IApiConnection
{
    internal ApiConnection(IConnection connection)
    {
        Connection = connection;
    }

    public IConnection Connection { get; }

    public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Get<T>(uri: uri, parameters: parameters, accepts: accepts, contentType: contentType, cancellationToken: cancellationToken);
    }

    public async ValueTask<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Patch(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async Task<T> Patch<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Patch<T>(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async ValueTask<HttpStatusCode> Post(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Post(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async Task<T> Post<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Post<T>(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async ValueTask<HttpStatusCode> Put(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Put(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async Task<T> Put<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Put<T>(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async ValueTask<HttpStatusCode> Delete(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Delete(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }

    public async Task<T> Delete<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));
        return await Connection.Delete<T>(uri: uri, body: body, parameters: parameters, cancellationToken: cancellationToken, accepts: accepts, contentType: contentType);
    }
}