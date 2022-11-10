using System.Net;

namespace Penzle.Core.Http;

public interface IConnection
{
    public string UserAgent { get; set; }
    public IHttpClient HttpClient { get; set; }
    Uri BaseAddress { get; set; }
    ICredentialStore<BearerCredentials> CredentialStore { get; set; }
    Credentials Credentials { get; set; }
    Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Patch(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Patch<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Post(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Post<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Put(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Put<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    ValueTask<HttpStatusCode> Delete(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    Task<T> Delete<T>(Uri uri, object body, IDictionary<string, string> parameters, string accepts, string contentType, CancellationToken cancellationToken = default);
    void SetRequestTimeout(TimeSpan timeout);
}
