using System.Text.Json;
using System.Text.Json.Serialization;

namespace Penzle.Core.Http.Internal;

public sealed class MicrosoftJsonSerializer : IJsonSerializer
{
    public JsonSerializerOptions Options { get; } = new() { PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.IgnoreCycles };

    public string Serialize(object item)
    {
        return JsonSerializer.Serialize(value: item, options: Options);
    }

    public TModel Deserialize<TModel>(string json)
    {
        return JsonSerializer.Deserialize<TModel>(json: json, options: Options);
    }

    public object Deserialize(string json, Type returnType)
    {
        return JsonSerializer.Deserialize(json: json, returnType: returnType, options: Options);
    }
}
