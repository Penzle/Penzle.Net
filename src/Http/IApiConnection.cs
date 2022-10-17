using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Penzle.Core.Http;

internal interface IApiConnection
{
    IConnection Connection { get; }
    Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Patch<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Post(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Post<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Put(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Put<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Delete(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Delete<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
}