﻿// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Clients;

/// <summary>
///     It represents the set of clients for the Content Entry Management API.
/// </summary>
public interface IManagementEntryClient
{
    /// <summary>
    ///     Creates content entry.
    /// </summary>
    /// <param name="entry">Represents content entry that will be created.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Guid" /> instance that represents the created content item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Guid> CreateEntry<TEntity>(CreateEntryRequest<TEntity> entry, CancellationToken cancellationToken = default) where TEntity : new();
    /// <summary>
    ///     Updated existing content entry by entry id.
    /// </summary>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="entry">Represents content entry that will be updated.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> UpdateEntry<TEntity>(Guid entryId, UpdateEntryRequest<TEntity> entry, CancellationToken cancellationToken = default) where TEntity : new();

    /// <summary>
    ///     Delete existing content entry by entry id.
    /// </summary>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> DeleteEntry(Guid entryId, CancellationToken cancellationToken = default);
}
