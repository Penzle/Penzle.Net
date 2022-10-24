using System;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Exceptions;

namespace Penzle.Core.Clients;

/// <summary>
///     Represents a collection of queries sent to the Content Form Delivery API.
/// </summary>
public interface IDeliveryFormClient
{
    /// <summary>
    ///     Retrieve a single form by its form id.
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
}