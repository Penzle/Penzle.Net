using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Penzle.Core.Clients.Abstract;
using Penzle.Core.Http;
using Penzle.Core.Utilities;

namespace Penzle.Core.Clients.Rest;

/// <inheritdoc cref="IFormClient" />
internal class RestFormClient : RestBaseClient, IFormClient
{
    public RestFormClient(IApiConnection apiConnection) : base(apiConnection: apiConnection)
    {
    }

    /// <inheritdoc cref="IFormClient.GetForm{TForm}" />
    public Task<TForm> GetForm<TForm>(Guid formId, string language = null, CancellationToken cancellationToken = default) where TForm : new()
    {
        return Connection.Get<TForm>(uri: ApiUrls.GetForm(formId: formId, language: language), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IFormClient.CreateForm" />
    public Task<Guid> CreateForm(object form, CancellationToken cancellationToken = default)
    {
        return Connection.Post<Guid>(uri: ApiUrls.CreateForm(), body: form, accepts: null, contentType: null, parameters: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IFormClient.UpdateForm" />
    public ValueTask<HttpStatusCode> UpdateForm(Guid formId, object form, CancellationToken cancellationToken = default)
    {
        return Connection.Put(uri: ApiUrls.UpdateForm(formId: formId), body: form, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IFormClient.DeleteForm" />
    public ValueTask<HttpStatusCode> DeleteForm(Guid formId, CancellationToken cancellationToken = default)
    {
        return Connection.Delete(uri: ApiUrls.DeleteForm(formId: formId), body: null, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }
}