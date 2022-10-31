using System.Text.Json.Serialization;

namespace Penzle.Core.Models;

public class Template : BaseSystem
{
    [JsonIgnore] public override bool HasWorkflowApplied { get; set; }
    [JsonIgnore] public override string RecentVersion { get; set; }
    [JsonIgnore] public override string UrlAlias { get; set; }
    [JsonIgnore] public override string AliasPath { get; set; }
    [JsonIgnore] public override string Language { get; set; }
    public string Type { get; set; }
    public List<BaseTemplates> Base { get; set; } = new();
    public IDictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}
