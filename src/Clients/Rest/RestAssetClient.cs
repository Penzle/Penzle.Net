using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Clients.Abstract;
using Penzle.Core.Http;
using Penzle.Core.Models;
using Penzle.Core.Utilities;

namespace Penzle.Core.Clients.Rest;

/// <inheritdoc cref="IAssetClient" />
internal class RestAssetClient : RestBaseClient, IAssetClient
{
    public RestAssetClient(IApiConnection apiConnection) : base(apiConnection: apiConnection)
    {
    }

    /// <inheritdoc cref="IAssetClient.GetAssets" />
    public Task<PagedList<Asset>> GetAssets(QueryAssetBuilder query = null, CancellationToken cancellationToken = default)
    {
        query ??= new QueryAssetBuilder();
        return Connection.Get<PagedList<Asset>>(uri: ApiUrls.GetAssets(parentId: query.ParentId, language: query.Language, keyword: query.Keyword, tag: query.Tag, mimeType: query.MimeType, page: query.Pagination.Page, pageSize: query.Pagination.PageSize, orderBy: null, direction: null, ids: query.Ids), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IAssetClient.GetAsset" />
    public Task<Asset> GetAsset(Guid id, string language = null, CancellationToken cancellationToken = default)
    {
        return Connection.Get<Asset>(uri: ApiUrls.GetAsset(id: id, language: language), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IAssetClient.AddAsset" />
    public Task<Guid> AddAsset(AddAssetRequest request, CancellationToken cancellationToken = default)
    {
        var streamContent = PrepareAsset(payload: request.Payload);
        streamContent.Add(content: new StringContent(content: request.Description), name: nameof(request.Description).ToLower());

        return Connection.Post<Guid>(uri: ApiUrls.AddAsset(folderId: request.FolderId, language: request.Language), body: streamContent, parameters: null, contentType: "multipart/form-data", accepts: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IAssetClient.UpdateAsset" />
    public ValueTask<HttpStatusCode> UpdateAsset(UpdateAssetRequest request, CancellationToken cancellationToken = default)
    {
        var streamContent = PrepareAsset(payload: request.Payload);
        return Connection.Put(uri: ApiUrls.UpdateAsset(id: request.Id, language: request.Language), body: streamContent, parameters: null, contentType: "multipart/form-data", accepts: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IAssetClient.DeleteAsset" />
    public ValueTask<HttpStatusCode> DeleteAsset(Guid id, CancellationToken cancellationToken = default)
    {
        return DeleteAssets(ids: new[] { id }, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IAssetClient.DeleteAssets" />
    public ValueTask<HttpStatusCode> DeleteAssets(Guid[] ids, CancellationToken cancellationToken = default)
    {
        return Connection.Delete(uri: ApiUrls.DeleteAssets(ids: ids), body: null, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    private static MultipartFormDataContent PrepareAsset(FileStream payload)
    {
        var fileName = Path.GetFileName(path: payload.Name);
        var extension = Path.GetExtension(path: fileName);

        var streamContent = new MultipartFormDataContent();

        var file = new StreamContent(content: payload);
        file.Headers.ContentType = new MediaTypeHeaderValue(mediaType: MimeType.Parse(value: extension).Type.FirstOrDefault());

        streamContent.Add(content: file, name: nameof(payload), fileName: fileName);
        return streamContent;
    }
}