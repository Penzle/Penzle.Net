namespace Penzle.Core.Models;

public sealed class AddAssetRequest
{
    public FileStream Payload { get; set; }
    public string Description { get; set; }
    public Guid FolderId { get; set; }
    public string Language { get; set; }
}
