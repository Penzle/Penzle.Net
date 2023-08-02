namespace Penzle.Core;

/// <summary>
///     A delivery client for the initial release of the Penzle application programming interface. You may obtain further
///     information on the API by visiting the following website: https://github.com/Penzle/Penzle.Net.
/// </summary>
public interface IDeliveryPenzleClient
{
    /// <summary>
    ///     A client for the Penzle Entry Content API that is responsible for handling delivery.
    /// </summary>
    IDeliveryEntryClient Entry { get; }

    /// <summary>
    ///     A client for the Penzle Form Content API that is responsible for handling delivery.
    /// </summary>
    IDeliveryFormClient Form { get; }

    /// <summary>
    ///     A client for the Penzle Template API that is responsible for handling delivery.
    /// </summary>
    IDeliveryTemplateClient Template { get; }

    /// <summary>
    ///     A client for the Penzle Asset API that is responsible for handling delivery.
    /// </summary>
    IDeliveryAssetClient Asset { get; }
}
