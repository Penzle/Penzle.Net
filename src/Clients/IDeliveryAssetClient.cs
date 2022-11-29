namespace Penzle.Core.Clients;

/// <summary>
///     Represents collection of Content Asset Management API queries.
/// </summary>
public interface IDeliveryAssetClient
{
    /// <summary>
    ///     Returns a collection of assets.
    ///     The asset management API returns a paginated listing response that is limited to 100 items.
    ///     To check if the next page is available use <see cref="PagedList{T}.HasNextPage" />.
    /// </summary>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="PagedList{Asset}" /> instance that represents the collection of assets.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<PagedList<Asset>> GetAssets(QueryAssetBuilder query = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns single asset by id.
    /// </summary>
    /// <param name="id">The identifier of the asset.</param>
    /// <param name="language">The language which are asset available.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="Asset" /> instance that represents requested asset.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Asset> GetAsset(Guid id, string language = null, CancellationToken cancellationToken = default);
}
