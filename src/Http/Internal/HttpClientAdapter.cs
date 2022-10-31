using System.Net.Http.Headers;
using System.Text;
using Penzle.Core.Utilities;

namespace Penzle.Core.Http.Internal;

public class HttpClientAdapter : IHttpClient
{
    public HttpClientAdapter()
    {
        Http = new HttpClient(new RedirectHandler());
    }

    public HttpClientAdapter(Func<HttpMessageHandler> getHandler)
    {
        Guard.ArgumentNotNull(getHandler, nameof(getHandler));
        Http = new HttpClient(new RedirectHandler {InnerHandler = getHandler()});
    }

    internal virtual HttpClient Http { get; }

    public virtual async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
    {
        Guard.ArgumentNotNull(request, nameof(request));

        var cancellationTokenForRequest = GetCancellationTokenForRequest(request, cancellationToken);

        using var requestMessage = BuildRequestMessage(request);
        var responseMessage = await SendAsync(requestMessage, cancellationTokenForRequest).ConfigureAwait(false);
        return await BuildResponse(responseMessage).ConfigureAwait(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void SetRequestTimeout(TimeSpan timeout)
    {
        Http.Timeout = timeout;
    }

    internal virtual CancellationToken GetCancellationTokenForRequest(IRequest request,
        CancellationToken cancellationToken)
    {
        var cancellationTokenForRequest = cancellationToken;

        if (request.Timeout == TimeSpan.Zero)
            return cancellationTokenForRequest;

        var timeoutCancellation = new CancellationTokenSource(request.Timeout);
        var unifiedCancellationToken =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellation.Token);

        cancellationTokenForRequest = unifiedCancellationToken.Token;
        return cancellationTokenForRequest;
    }

    internal virtual async Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
    {
        Guard.ArgumentNotNull(responseMessage, nameof(responseMessage));

        object responseBody = null;
        string contentType = null;

        using (var content = responseMessage.Content)
        {
            if (content != null)
            {
                contentType = GetContentAssetType(responseMessage.Content);
                responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        var responseHeaders = responseMessage.Headers.ToDictionary(h => h.Key, h => h.Value.First());
        return new Response(responseMessage.StatusCode, responseBody, responseHeaders, contentType);
    }

    internal virtual HttpRequestMessage BuildRequestMessage(IRequest request)
    {
        Guard.ArgumentNotNull(request, nameof(request));
        HttpRequestMessage requestMessage = null;
        try
        {
            var fullUri = new Uri(request.BaseAddress, request.Endpoint);
            requestMessage = new HttpRequestMessage(request.Method, fullUri);

            foreach (var header in request.Headers) requestMessage.Headers.Add(header.Key, header.Value);

            switch (request.Body)
            {
                case HttpContent httpContent:
                    requestMessage.Content = httpContent;
                    break;
                case string body:
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, request.ContentType);
                    break;
                case Stream bodyStream:
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
                    break;
            }
        }
        catch (Exception)
        {
            requestMessage?.Dispose();
            throw;
        }

        return requestMessage;
    }

    internal virtual string GetContentAssetType(HttpContent httpContent)
    {
        return httpContent.Headers is {ContentType: { }} ? httpContent.Headers.ContentType.MediaType : null;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        Http?.Dispose();
    }

    internal virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await Http.SendAsync(request, cancellationToken).ConfigureAwait(false);
        return response;
    }
}