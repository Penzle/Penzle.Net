using System;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;

namespace Penzle.Core.Clients;

/// <summary>
///     Represents set of Template Management API requests.
/// </summary>
public interface ITemplateClient
{
    /// <summary>
    ///     Retrieve a single template by its template id.
    /// </summary>
    /// <param name="templateId">The ID of the entry.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Template> GetTemplate(Guid templateId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Retrieve a single template by its template code name.
    /// </summary>
    /// <param name="codeName">The code of the template.</param>
    /// <param name="cancellationToken">The optional cancellation token to cancel the operation.</param>
    /// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
    Task<Template> GetTemplateByCodeName(string codeName, CancellationToken cancellationToken = default);
}