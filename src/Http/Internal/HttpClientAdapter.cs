using System.Net.Http.Headers;
using System.Text;
using Penzle.Core.Utilities;

namespace Penzle.Core.Http.Internal;

public class HttpClientAdapter : IHttpClient
{
    public HttpClientAdapter()
    {
        Http = new HttpClient(handler: new RedirectHandler());
    }

    public HttpClientAdapter(Func<HttpMessageHandler> getHandler)
    {
        Guard.ArgumentNotNull(value: getHandler, name: nameof(getHandler));
        Http = new HttpClient(handler: new RedirectHandler { InnerHandler = getHandler() });
    }

    internal virtual HttpClient Http { get; }

    public virtual async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
    {
        Guard.ArgumentNotNull(value: request, name: nameof(request));

        var cancellationTokenForRequest = GetCancellationTokenForRequest(request: request, cancellationToken: cancellationToken);

        using var requestMessage = BuildRequestMessage(request: request);
        var responseMessage = await SendAsync(request: requestMessage, cancellationToken: cancellationTokenForRequest).ConfigureAwait(continueOnCapturedContext: false);
        return await BuildResponse(responseMessage: responseMessage).ConfigureAwait(continueOnCapturedContext: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(obj: this);
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
        {
            return cancellationTokenForRequest;
        }

        var timeoutCancellation = new CancellationTokenSource(delay: request.Timeout);
        var unifiedCancellationToken =
            CancellationTokenSource.CreateLinkedTokenSource(token1: cancellationToken, token2: timeoutCancellation.Token);

        cancellationTokenForRequest = unifiedCancellationToken.Token;
        return cancellationTokenForRequest;
    }

    internal virtual async Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
    {
        Guard.ArgumentNotNull(value: responseMessage, name: nameof(responseMessage));

        object responseBody = null;
        string contentType = null;

        using (var content = responseMessage.Content)
        {
            if (content != null)
            {
                contentType = GetContentAssetType(httpContent: responseMessage.Content);
                responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        var responseHeaders = responseMessage.Headers.ToDictionary(keySelector: h => h.Key, elementSelector: h => h.Value.First());
        return new Response(statusCode: responseMessage.StatusCode, body: responseBody, headers: responseHeaders, contentType: contentType);
    }

    internal virtual HttpRequestMessage BuildRequestMessage(IRequest request)
    {
        Guard.ArgumentNotNull(value: request, name: nameof(request));
        HttpRequestMessage requestMessage = null;
        try
        {
            var fullUri = new Uri(baseUri: request.BaseAddress, relativeUri: request.Endpoint);
            requestMessage = new HttpRequestMessage(method: request.Method, requestUri: fullUri);

            foreach (var header in request.Headers)
            {
                requestMessage.Headers.Add(name: header.Key, value: header.Value);
            }

            switch (request.Body)
            {
                case HttpContent httpContent:
                    requestMessage.Content = httpContent;
                    break;
                case string body:
                    requestMessage.Content = new StringContent(content: body, encoding: Encoding.UTF8, mediaType: request.ContentType);
                    break;
                case Stream bodyStream:
                    requestMessage.Content = new StreamContent(content: bodyStream);
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType: request.ContentType);
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
        return httpContent.Headers is { ContentType: { } } ? httpContent.Headers.ContentType.MediaType : null;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        Http?.Dispose();
    }

    internal virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await Http.SendAsync(request: request, cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        return response;
    }
}
