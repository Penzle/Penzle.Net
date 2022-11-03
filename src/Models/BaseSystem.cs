using System.Text.Json.Serialization;

namespace Penzle.Core.Models;

public class BaseSystem
{
    public virtual string Template { get; set; }
    public virtual Guid Id { get; set; }
    public virtual Guid ParentId { get; set; }
    public virtual string Name { get; set; }
    public virtual string Version { get; set; }
    [JsonPropertyName(name: "language")] public virtual string Language { get; set; }
    public virtual string AliasPath { get; set; }
    public virtual string Slug { get; set; }
    public virtual DateTime ModifiedAt { get; set; }
    public virtual DateTime CreatedAt { get; set; }
}
