using System.Text.Json.Serialization;

namespace Penzle.Core.Models;

public abstract class EntrySystem : BaseSystem
{
    [JsonPropertyName(name: "template")] public override string Template { get; set; }
}