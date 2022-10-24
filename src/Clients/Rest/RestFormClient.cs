using System.Net;
using Penzle.Core.Clients.Abstract;
using Penzle.Core.Http;
using Penzle.Core.Utilities;

namespace Penzle.Core.Clients.Rest;

/// <summary>
///     Represent a REST form client that contains application programming interfaces (APIs) for management and delivery
///     methods.
/// </summary>
internal sealed class RestFormClient : RestBaseClient, IManagementFormClient, IDeliveryFormClient
{
    public RestFormClient(IApiConnection apiConnection) : base(apiConnection: apiConnection)
    {
    }

    /// <inheritdoc>
    ///     <cref>IManagementFormClient.GetForm{TForm}</cref>
    /// </inheritdoc>
    public Task<TForm> GetForm<TForm>(Guid formId, string language = null, CancellationToken cancellationToken = default) where TForm : new()
    {
        return Connection.Get<TForm>(uri: ApiUrls.GetForm(formId: formId, language: language), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IManagementFormClient.CreateForm" />
    public Task<Guid> CreateForm(object form, CancellationToken cancellationToken = default)
    {
        return Connection.Post<Guid>(uri: ApiUrls.CreateForm(), body: form, accepts: null, contentType: null, parameters: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IManagementFormClient.UpdateForm" />
    public ValueTask<HttpStatusCode> UpdateForm(Guid formId, object form, CancellationToken cancellationToken = default)
    {
        return Connection.Put(uri: ApiUrls.UpdateForm(formId: formId), body: form, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    /// <inheritdoc cref="IManagementFormClient.DeleteForm" />
    public ValueTask<HttpStatusCode> DeleteForm(Guid formId, CancellationToken cancellationToken = default)
    {
        return Connection.Delete(uri: ApiUrls.DeleteForm(formId: formId), body: null, parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }
}