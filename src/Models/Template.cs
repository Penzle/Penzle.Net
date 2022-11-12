namespace Penzle.Core.Models;

public class Template : BaseSystem
{
    [JsonIgnore] public override string Version { get; set; }
    [JsonIgnore] public override string Slug { get; set; }
    [JsonIgnore] public override string AliasPath { get; set; }
    [JsonIgnore] public override string Language { get; set; }
    public string Type { get; set; }
    public List<BaseTemplates> Base { get; set; } = new();
    public IDictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}
