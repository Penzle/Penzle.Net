// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.
using CSharpFunctionalExtensions;
namespace Penzle.Core.Clients;

/// <summary>
///     A client for the Penzle User API that is responsible for handling user-related operations.
/// </summary>
public interface IManagementUserClient
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

    /// <summary>
    ///     Adds a user with the specified user object.
    /// </summary>
    /// <param name="user">The user object containing user details.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <exception cref="PenzleException">There was a communication error with the Penzle API.</exception>
    Task<Result<Guid>> EnrollUser(User user, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a user with the specified user name, email, first name, and last name.
    /// </summary>
    /// <param name="userName">The user's user name.</param>
    /// <param name="email">The user's email address.</param>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <exception cref="PenzleException">There was a communication error with the Penzle API.</exception>
    Task<Result<Guid>> EnrollUser(string userName, string email, string firstName, string lastName, CancellationToken cancellationToken = default);
}
