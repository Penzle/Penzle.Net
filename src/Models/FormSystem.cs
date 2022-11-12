namespace Penzle.Core.Models;

public sealed class FormSystem : BaseSystem
{
    [JsonPropertyName(name: "form")] public override string Template { get; set; }
    [JsonIgnore] public override string Slug { get; set; }
}
