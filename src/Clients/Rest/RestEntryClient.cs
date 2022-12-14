namespace Penzle.Core.Clients.Rest;

/// <summary>
///     Represent a REST entry client that contains application programming interfaces (APIs) for management and delivery
///     methods.
/// </summary>
internal sealed class RestEntryClient : RestBaseClient, IManagementEntryClient, IDeliveryEntryClient
{
    public RestEntryClient(IApiConnection apiConnection) : base(apiConnection: apiConnection)
    {
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public Task<PagedList<TEntry>> GetPaginationListEntries<TEntry>(QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new()
    {
        var template = typeof(TEntry).IsGenericType ? typeof(TEntry).GenericTypeArguments[0].Name : typeof(TEntry).Name;
        return GetPaginationListEntries<TEntry>(template: template, query: query, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public async Task<PagedList<TEntry>> GetPaginationListEntries<TEntry>(string template, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new()
    {
        query ??= new QueryEntryBuilder();
        if (typeof(TEntry).IsGenericType)
        {
            return await Connection.Get<PagedList<TEntry>>(uri: ApiUrls.GetEntries(template: template, parentId: query.ParentId, language: query.Language, ids: query.Ids, page: query.Pagination.Page, pageSize: query.Pagination.PageSize), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
        }

        var response = await Connection.Get<PagedList<Entry<TEntry>>>(uri: ApiUrls.GetEntries(template: template, parentId: query.ParentId, language: query.Language, ids: query.Ids, page: query.Pagination.Page, pageSize: query.Pagination.PageSize), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
        return new PagedList<TEntry>
        {
            Items = response.Items.Select(selector: entry => entry.Fields).ToArray(),
            PageIndex = response.PageIndex,
            PageSize = response.PageSize,
            TotalCount = response.TotalCount,
            TotalPages = response.TotalPages,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage
        };
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public Task<TEntry> GetEntry<TEntry>(Guid entryId, string language = null, CancellationToken cancellationToken = default) where TEntry : new()
    {
        return Connection.Get<TEntry>(uri: ApiUrls.GetEntry(entryId: entryId, language: language), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public Task<TEntry> GetEntry<TEntry>(string uri, string language = null, CancellationToken cancellationToken = default) where TEntry : new()
    {
        return Connection.Get<TEntry>(uri: ApiUrls.GetEntryByAliasUrl(uri: uri, language: language), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(QueryEntryBuilder query = null, int fetch = 50, CancellationToken cancellationToken = default) where TEntry : new()
    {
        var template = typeof(TEntry).IsGenericType ? typeof(TEntry).GenericTypeArguments[0].Name : typeof(TEntry).Name;
        return GetEntries<TEntry>(template: template, fetch: fetch, query: query, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.GetEntry{TEntry}(System.Guid,Penzle.Core.Models.QueryEntryBuilder,System.Threading.CancellationToken)</cref>
    /// </inheritdoc>
    public async Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(string template, QueryEntryBuilder query = null, int fetch = 50, CancellationToken cancellationToken = default) where TEntry : new()
    {
        query ??= new QueryEntryBuilder();
        query.Pagination.WithPageSize(pageSize: fetch);

        if (typeof(TEntry).IsGenericType)
        {
            var pagedList = await Connection.Get<PagedList<TEntry>>(uri: ApiUrls.GetEntries(template: template, parentId: query.ParentId, language: query.Language, ids: query.Ids, page: query.Pagination.Page, pageSize: query.Pagination.PageSize), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
            return pagedList.Items.Select(selector: entry => entry).ToList();
        }
        else
        {
            var pagedList = await Connection.Get<PagedList<Entry<TEntry>>>(uri: ApiUrls.GetEntries(template: template, parentId: query.ParentId, language: query.Language, ids: query.Ids, page: query.Pagination.Page, pageSize: query.Pagination.PageSize), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
            return pagedList.Items.Select(selector: entry => entry.Fields).ToList();
        }
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.CreateEntry</cref>
    /// </inheritdoc>
    public Task<Guid> CreateEntry(object entry, CancellationToken cancellationToken = default)
    {
        return Connection.Post<Guid>(uri: ApiUrls.CreateEntry(), body: entry, accepts: null, contentType: null, parameters: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.UpdateEntry</cref>
    /// </inheritdoc>
    public ValueTask<HttpStatusCode> UpdateEntry(Guid entryId, object entry, CancellationToken cancellationToken = default)
    {
        return Connection.Put(uri: ApiUrls.UpdateEntry(entryId: entryId), body: entry, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc>
    ///     <cref>IManagementEntryClient.DeleteEntry</cref>
    /// </inheritdoc>
    public ValueTask<HttpStatusCode> DeleteEntry(Guid entryId, CancellationToken cancellationToken = default)
    {
        return Connection.Delete(uri: ApiUrls.DeleteEntry(entryId: entryId), body: null, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }
}
