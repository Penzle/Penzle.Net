namespace Penzle.Core.Clients;

/// <summary>
///     Retrieves a paginated list of users, optionally filtered by a querystring. Utilizing the class is more
///     efficient than manually constructing a query. See <see cref="QueryUserBuilder" />.
/// </summary>
public interface IDeliveryUserClient
{
    /// <summary>
    ///     Retrieves a paginated list of users.
    /// </summary>
    /// <typeparam name="TUserResponse">The class into which to serialize the response.</typeparam>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <returns>A PagedList of users.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle API.</exception>
    Task<PagedList<TUserResponse>> GetPaginatedUserList<TUserResponse>(QueryUserBuilder query = null, CancellationToken cancellationToken = default)
        where TUserResponse : new();

    /// <summary>
    ///     Retrieves a single user by their user ID.
    /// </summary>
    /// <typeparam name="TUserResponse">The type into which to serialize this user.</typeparam>
    /// <param name="targetUserId">The ID of the user.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TUserResponse" /> object representing the user.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle API.</exception>
    Task<TUserResponse> GetUser<TUserResponse>(Guid targetUserId, CancellationToken cancellationToken = default) where TUserResponse : new();
}
