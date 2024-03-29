﻿namespace Penzle.Core.Http;

/// <summary>
///     It is a representation of an interface that is intended to send requests to an HTTP server.
/// </summary>
public interface IRequest
{
    /// <summary>
    ///     Gets or set the body or payload for this request.
    /// </summary>
    object Body { get; set; }

    /// <summary>
    ///     Gets the headers for this request.
    /// </summary>
    Dictionary<string, string> Headers { get; set; }

    /// <summary>
    ///     Set the http method of the request such as GET, POST, PUT, DELETE, PATCH
    /// </summary>
    HttpMethod Method { get; set; }

    /// <summary>
    ///     Gets the parameters for this request.
    /// </summary>
    Dictionary<string, string> Parameters { get; set; }

    /// <summary>
    ///     Gets the URL of the Internet resource that is used when the query is specified relative address.
    /// </summary>
    Uri BaseAddress { get; set; }

    /// <summary>
    ///     Relative URL of target resource.
    /// </summary>
    Uri Endpoint { get; set; }

    /// <summary>
    ///     Gets e wait time in TimeSpan when connecting to the HTTP-server.
    /// </summary>
    TimeSpan Timeout { get; set; }

    /// <summary>
    ///     The Content-Type header is used to indicate the media type of the resource.
    /// </summary>
    string ContentType { get; set; }
}
