using System.Net;

namespace Penzle.Core.Http;

/// <summary>
///     Represents a generic HTTP response
/// </summary>
public interface IResponse
{
    /// <summary>
    ///     Raw response body. Typically a string.
    /// </summary>
    object Body { get; set; }

    /// <summary>
    ///     Information about the API.
    /// </summary>
    IReadOnlyDictionary<string, string> Headers { get; set; }

    /// <summary>
    ///     The response status code.
    /// </summary>
    HttpStatusCode StatusCode { get; set; }

    /// <summary>
    ///     The content type of the response.
    /// </summary>
    string ContentType { get; set; }
}