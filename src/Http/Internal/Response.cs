namespace Penzle.Core.Http.Internal;

/// <summary>
///     Represents a generic HTTP response
/// </summary>
internal class Response : IResponse
{
    public Response(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
    {
        Guard.ArgumentNotNull(value: headers, name: nameof(headers));

        StatusCode = statusCode;
        Body = body;
        Headers = new ReadOnlyDictionary<string, string>(dictionary: headers);
        ContentType = contentType;
    }

    /// <summary>
    ///     Raw response body. Typically a string, but when requesting images, it will be a byte array.
    /// </summary>
    public object Body { get; set; }

    /// <summary>
    ///     Information about the API.
    /// </summary>
    public IReadOnlyDictionary<string, string> Headers { get; set; }

    /// <summary>
    ///     The response status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     The content type of the response.
    /// </summary>
    public string ContentType { get; set; }
}
