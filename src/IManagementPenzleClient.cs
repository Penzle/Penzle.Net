namespace Penzle.Core;

/// <summary>
///     A management client for the initial release of the Penzle application programming interface. You may obtain further
///     information on the API by visiting the following website: http://doc.penzle.com/sdk.
/// </summary>
public interface IManagementPenzleClient
{
    /// <summary>
    ///     A client for the Penzle Entry Content API that is responsible for handling management.
    /// </summary>
    IManagementEntryClient Entry { get; }

    /// <summary>
    ///     A client for the Penzle Form Content API that is responsible for handling management.
    /// </summary>
    IManagementFormClient Form { get; }

    /// <summary>
    ///     A client for the Penzle Asset Content API that is responsible for handling management.
    /// </summary>
    IManagementAssetClient Asset { get; }

    /// <summary>
    ///     A client for the Penzle User API that is responsible for handling user-related operations.
    /// </summary>
    IManagementUserClient User { get; }
}
