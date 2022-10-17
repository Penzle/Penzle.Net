using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Exceptions;

namespace Penzle.Core.Clients;

/// <summary>
///     Represents set of Content Form Management API requests.
/// </summary>
public interface IFormClient
{
    /// <summary>
    ///     Retrieve a single form by its entry id.
    /// </summary>
    /// <typeparam name="TForm">
    ///     The type into which to serialize this form. If you wish to include metadata in the serialized response.
    /// </typeparam>
    /// <param name="formId">The ID of the entry.</param>
    /// <param name="language">The optional querystring to add additional filtering to the query in terms of language.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="TForm" /> of item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<TForm> GetForm<TForm>(Guid formId, string language = null, CancellationToken cancellationToken = default) where TForm : new();

    /// <summary>
    ///     Creates content form.
    /// </summary>
    /// <param name="form">Represents content from that will be created.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="Guid" /> instance that represents the created form item.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Guid> CreateForm(object form, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updated existing form entry by form id.
    /// </summary>
    /// <param name="formId">The ID of the form.</param>
    /// <param name="form">Represents form that will be updated.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> UpdateForm(Guid formId, object form, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete existing form by form id.
    /// </summary>
    /// <param name="formId">The ID of the entry.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <returns>The <see cref="HttpStatusCode" /> instance that represents the status code of http request.</returns>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    ValueTask<HttpStatusCode> DeleteForm(Guid formId, CancellationToken cancellationToken = default);
}