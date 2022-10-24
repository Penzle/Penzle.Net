using System.Text.Json.Serialization;

namespace Penzle.Core.Models;

public abstract class BaseSystem
{
    public virtual string Template { get; set; }
    public virtual Guid Id { get; set; }
    public virtual Guid ParentId { get; set; }
    public virtual string Name { get; set; }
    public virtual string RecentVersion { get; set; }
    [JsonPropertyName(name: "language")] public virtual string Language { get; set; }
    public virtual string AliasPath { get; set; }
    public virtual string UrlAlias { get; set; }
    public virtual bool EnabledUrlAlias { get; set; }
    public virtual bool EnabledParentAliasInheritance { get; set; }
    public virtual bool IsCheckedOut { get; set; }
    public virtual bool HasWorkflowApplied { get; set; }
    public virtual string Icon { get; set; }
}