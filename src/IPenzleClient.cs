using Penzle.Core.Clients;

namespace Penzle.Core;

/// <summary>
///     A Client for the Penzle API v1. You can read more about the API here: http://doc.penzle.com/sdk.
/// </summary>
public interface IPenzleClient
{
    /// <summary>
    ///     A client for Penzle Entry Content API
    /// </summary>
    /// <remarks>
    ///     See the <a href="https://doc.penzle.com/content/data-content">Check Entry Content documentation</a> for more
    ///     information.
    /// </remarks>
    IEntryClient Entry { get; }

    /// <summary>
    ///     A client for Penzle Form API
    /// </summary>
    /// <remarks>
    ///     See the <a href="https://doc.penzle.com/content/form-content">Form Content documentation</a> for more
    ///     information.
    /// </remarks>
    IFormClient Form { get; }

    /// <summary>
    ///     A client for Penzle Template API
    /// </summary>
    /// <remarks>
    ///     See the <a href="https://doc.penzle.com/templates/data-templates">Check templates documentation</a> for more
    ///     information.
    /// </remarks>
    ITemplateClient Template { get; }

    /// <summary>
    ///     A client for Asset API
    /// </summary>
    /// <remarks>
    ///     See the <a href="https://doc.penzle.com/media">Check templates documentation</a> for more
    ///     information.
    /// </remarks>
    IAssetClient Asset { get; }
}