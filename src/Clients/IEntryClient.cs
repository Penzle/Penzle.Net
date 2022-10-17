using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;

namespace Penzle.Core.Clients;

/// <summary>
///     Represents set of Content Entry Management API requests.
/// </summary>
public interface IEntryClient
{
    /// <summary>
    ///     Retrieves all the entries of a template, optionally filtered by a querystring. Utilizing the class is more
    ///     efficient
    ///     than manually constructing a query. See <see cref="QueryEntryBuilder" />.
    /// </summary>
    /// <typeparam name="TEntry">The class into which to serialize the response.</typeparam>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <param name="template">The template code representing the object's shape from which has been created.</param>
    /// <returns>A <see cref="TEntry" /> of items.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<PagedList<TEntry>> GetPaginationListEntries<TEntry>(string template, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();

    /// <summary>
    ///     Retrieves all the entries of a template, optionally filtered by a querystring. Utilizing the class is more
    ///     efficient
    ///     than manually constructing a query. See <see cref="QueryEntryBuilder" />.
    /// </summary>
    /// <typeparam name="TEntry">The class into which to serialize the response.</typeparam>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>A <see cref="TEntry" /> of items.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<PagedList<TEntry>> GetPaginationListEntries<TEntry>(QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();


    /// <summary>
    ///     Retrieves all the entries of a template, optionally filtered by a querystring. Utilizing the class is more
    ///     efficient
    ///     than manually constructing a query. See <see cref="QueryEntryBuilder" />.
    /// </summary>
    /// <typeparam name="TEntry">The class into which to serialize the response.</typeparam>
    /// <param name="fetch">The maximum items which will be returned in collection.</param>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>A read only collection of <see cref="TEntry" /> of items.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(int fetch = 50, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();

    /// <summary>
    ///     Retrieves all the entries of a template, optionally filtered by a querystring. Utilizing the class is more
    ///     efficient
    ///     than manually constructing a query. See <see cref="QueryEntryBuilder" />.
    /// </summary>
    /// <typeparam name="TEntry">The class into which to serialize the response.</typeparam>
    /// <param name="template">The template code representing the object's shape from which has been created.</param>
    /// <param name="fetch">The maximum items which will be returned in collection.</param>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional token used to cancel an operation.</param>
    /// <returns>A read only collection of <see cref="TEntry" /> of items.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(string template, int fetch = 50, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();


    /// <summary>
    ///     Retrieve a single entry by its entry id.
    /// </summary>
    /// <typeparam name="TEntry">
    ///     The type into which to serialize this entry.If you wish to include metadata in the serialized response.
    /// </typeparam>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TEntry" /> of item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<TEntry> GetEntry<TEntry>(Guid entryId, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();

    /// <summary>
    ///     Retrieve a single entry by its alias url.
    /// </summary>
    /// <typeparam name="TEntry">
    ///     The type into which to serialize this entry.If you wish to include metadata in the serialized response.
    /// </typeparam>
    /// <param name="uri">The alias url of the entry.</param>
    /// <param name="query">The optional querystring to add additional filtering to the query.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TEntry" /> of item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<TEntry> GetEntry<TEntry>(string uri, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();

    /// <summary>
    ///     Creates content entry.
    /// </summary>
    /// <param name="entry">Represents content entry that will be created.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Guid" /> instance that represents the created content item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Guid> CreateEntry(object entry, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updated existing content entry by entry id.
    /// </summary>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="entry">Represents content entry that will be updated.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> UpdateEntry(Guid entryId, object entry, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete existing content entry by entry id.
    /// </summary>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> DeleteEntry(Guid entryId, CancellationToken cancellationToken = default);
}