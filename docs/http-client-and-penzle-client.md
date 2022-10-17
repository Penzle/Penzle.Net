## **Http Client and Penzle Client**

Continue reading if you are interested in learning more about the workings of Penzle Client and how it makes use of Http
Client. Otherwise, you should be fine if you stick with the default settings.

The Penzle Client was developed with the intention of accommodating the user's own particular HTTP Client
implementation. If you want to make something that is different from what everyone else is making, we provide an
interface that lets you inject your own implementation. If you have already set up the client using
the `HttpClientFactory`, you can pass this instance to share the same object while adhering to the general rules of your
application. These rules include things like content types, headers, and other similar things. The signature of the
interface you see in the next section and navigate them following namespace: `Penzle.Net.Http.IHttpClient`

```csharp
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

```

In the event that your application adheres to a strict time limit for response delivery, you have the ability to pass a
custom response time timeout value. By doing so, you can ensure that Penzle Client will throw an
exception `PenzleException` once the time limit has been reached.