namespace Penzle.Core.Models;

public class Asset
{
    public string Url { get; set; }
    public string AssetId { get; set; }
    public Guid? WorkflowId { get; set; }
    public AssetMimeType AssetMimeType { get; set; }
    public List<Tag> Tags { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Type { get; set; }
    public int Size { get; set; }
}
