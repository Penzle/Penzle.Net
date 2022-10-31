using System.Text.Json.Serialization;

namespace Penzle.Core.Models;

public sealed class FormSystem : BaseSystem
{
    [JsonPropertyName(name: "form")] public override string Template { get; set; }
    [JsonIgnore] public override bool EnabledParentAliasInheritance { get; set; }
    [JsonIgnore] public override bool EnabledUrlAlias { get; set; }
    [JsonIgnore] public override string UrlAlias { get; set; }
}
