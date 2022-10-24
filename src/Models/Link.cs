namespace Penzle.Core.Models;

public class Link
{
    public string Description { get; set; }
    public string Url { get; set; }
    public string Target { get; set; }
    public string Anchor { get; set; }
    public string QueryString { get; set; }
    public Guid? NodeId { get; set; }
    public string Type { get; set; }
}