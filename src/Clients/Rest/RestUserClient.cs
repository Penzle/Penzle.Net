namespace Penzle.Core.Clients.Rest;

/// <summary>
///     Provides REST-based implementation for user operations.
/// </summary>
internal sealed class RestUserClient : RestBaseClient, IDeliveryUserClient
{
    /// <summary>
    ///     Creates a new instance of the RestUserClient class.
    /// </summary>
    /// <param name="apiConnection">The API connection to be used for requests.</param>
    public RestUserClient(IApiConnection apiConnection) : base(apiConnection)
    {
    }

    /// <summary>
    ///     Retrieves a paginated list of users, optionally filtered by a query.
    /// </summary>
    /// <typeparam name="TUserResponse">The type into which to deserialize the response.</typeparam>
    /// <param name="query">The optional user query to filter results.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a PagedList of users.
    /// </returns>
    public Task<PagedList<TUserResponse>> GetPaginatedUserList<TUserResponse>(QueryUserBuilder query = null, CancellationToken cancellationToken = default) where TUserResponse : new()
    {
        query ??= new QueryUserBuilder();
        return Connection.Get<PagedList<TUserResponse>>(ApiUrls.GetUsers(query.Keyword, query.Pagination.Page, query.Pagination.PageSize), null, null, null, cancellationToken);
    }

    /// <summary>
    ///     Retrieves a single user by their user ID.
    /// </summary>
    /// <typeparam name="TUserResponse">The type into which to deserialize the response.</typeparam>
    /// <param name="targetUserId">The ID of the user.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a <see cref="TUserResponse" />
    ///     object representing the user.
    /// </returns>
    public Task<TUserResponse> GetUser<TUserResponse>(Guid targetUserId, CancellationToken cancellationToken = default) where TUserResponse : new()
    {
        return Connection.Get<TUserResponse>(ApiUrls.GetUser(targetUserId), null, null, null, cancellationToken);
    }
}
