namespace Penzle.Core.Models;

public sealed class DeleteAssetRequest
{
    public DeleteAssetRequest(Guid id)
    {
        Ids.Add(item: id);
    }

    public DeleteAssetRequest(IList<Guid> ids)
    {
        Ids = ids;
    }

    private IList<Guid> Ids { get; } = new List<Guid>();
}
