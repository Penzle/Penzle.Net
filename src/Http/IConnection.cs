using System.Net;

namespace Penzle.Core.Http;

public interface IConnection
{
    public string PlatformInformation { get; set; }
    public string UserAgent { get; }
    public IHttpClient HttpClient { get; }
    Uri BaseAddress { get; }
    ICredentialStore<BearerCredentials> CredentialStore { get; }
    Credentials Credentials { get; }
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