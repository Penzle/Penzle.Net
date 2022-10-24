using System.Net;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;

namespace Penzle.Core.Clients;

/// <summary>
///     Represents the collection of queries sent to the Content Asset Management API.
/// </summary>
public interface IManagementAssetClient
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

    /// <summary>
    ///     Creates asset item.
    /// </summary>
    /// <param name="request">Represents the asset request that will be created.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="Guid" /> instance of id that represents created asset.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Guid> AddAsset(AddAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update asset item by id.
    /// </summary>
    /// <param name="request">Represents the asset request that will be updated.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents http response of request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> UpdateAsset(UpdateAssetRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes the given asset by id.
    /// </summary>
    /// <param name="id">The identifier of the asset.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents http response of request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> DeleteAsset(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    ///     Deletes the given assets by referent id.
    /// </summary>
    /// <param name="ids">The collection of identifier of the assets.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents http response of request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> DeleteAssets(Guid[] ids, CancellationToken cancellationToken = default);
}