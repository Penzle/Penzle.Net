using Penzle.Core.Exceptions;
using Penzle.Core.Models;

namespace Penzle.Core.Clients;

/// <summary>
///     It represents a set of clients for the Content Entry Delivery API.
/// </summary>
public interface IDeliveryEntryClient
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
    Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(QueryEntryBuilder query = null, int fetch = 50, CancellationToken cancellationToken = default) where TEntry : new();

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
    Task<IReadOnlyList<TEntry>> GetEntries<TEntry>(string template, QueryEntryBuilder query = null, int fetch = 50, CancellationToken cancellationToken = default) where TEntry : new();


    /// <summary>
    ///     Retrieve a single entry by its entry id.
    /// </summary>
    /// <typeparam name="TEntry">
    ///     The type into which to serialize this entry.If you wish to include metadata in the serialized response.
    /// </typeparam>
    /// <param name="entryId">The ID of the entry.</param>
    /// <param name="language">The optional querystring to add additional filtering to the language.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TEntry" /> of item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<TEntry> GetEntry<TEntry>(Guid entryId, string language = null, CancellationToken cancellationToken = default) where TEntry : new();

    /// <summary>
    ///     Retrieve a single entry by its alias url.
    /// </summary>
    /// <typeparam name="TEntry">
    ///     The type into which to serialize this entry.If you wish to include metadata in the serialized response.
    /// </typeparam>
    /// <param name="uri">The alias url of the entry.</param>
    /// <param name="language">The optional querystring to add additional filtering to the language.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TEntry" /> of item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<TEntry> GetEntry<TEntry>(string uri, string language = null, CancellationToken cancellationToken = default) where TEntry : new();
}
