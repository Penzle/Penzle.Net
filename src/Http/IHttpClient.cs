using System;
using System.Threading;
using System.Threading.Tasks;

namespace Penzle.Core.Http;

/// <summary>
///     A general-purpose HTTP client. Useful resources for those seeking to influence default change. Uses its own version
///     of HttpClient to do advance stuff.
/// </summary>
public interface IHttpClient : IDisposable
{
    /// <summary>
    ///     Sends the request that has been specified and returns a response.
    /// </summary>
    /// <param name="request">A <see cref="IRequest" /> that represents the HTTP request</param>
    /// <param name="cancellationToken">Used to cancel the request</param>
    /// <returns>A <see cref="IResponse" /> of <see cref="IRequest" /></returns>
    Task<IResponse> Send(IRequest request, CancellationToken cancellationToken);


    /// <summary>
    ///     The connection between the client and the server will have its timeout set by this command.
    /// </summary>
    /// <param name="timeout">The Timeout value</param>
    void SetRequestTimeout(TimeSpan timeout);
}