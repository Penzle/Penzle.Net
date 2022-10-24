using Penzle.Core.Clients.Abstract;
using Penzle.Core.Http;
using Penzle.Core.Models;
using Penzle.Core.Utilities;

namespace Penzle.Core.Clients.Rest;

internal sealed class RestTemplateClient : RestBaseClient, IDeliveryTemplateClient
{
    public RestTemplateClient(IApiConnection apiConnection) : base(apiConnection: apiConnection)
    {
    }

    public Task<Template> GetTemplate(Guid templateId, CancellationToken cancellationToken = default)
    {
        return Connection.Get<Template>(uri: ApiUrls.GetTemplate(templateId: templateId), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }

    public Task<Template> GetTemplateByCodeName(string codeName, CancellationToken cancellationToken = default)
    {
        return Connection.Get<Template>(uri: ApiUrls.GetTemplateByCodeName(codeName: codeName), parameters: null, accepts: null, contentType: null, cancellationToken: cancellationToken);
    }
}