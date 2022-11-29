namespace Penzle.Core.Models;

[Serializable]
public class ApiError : ISerializable
{
    private static readonly IJsonSerializer s_jsonSerializer = new MicrosoftJsonSerializer();

    public ApiError()
    {
    }

    public ApiError(string title)
    {
        Title = title;
    }

    protected ApiError(SerializationInfo serializationInfo, StreamingContext streamingContext)
    {
        Title = serializationInfo.GetString(name: nameof(Title));
        Detail = serializationInfo.GetString(name: nameof(Detail));
        Instance = serializationInfo.GetString(name: nameof(Instance));
    }

    public ApiError(JsonObject jsonNode)
    {
        if (jsonNode.ContainsKey(propertyName: nameof(Title).ToLower()))
        {
            Title = jsonNode[propertyName: nameof(Title).ToLower()]?.ToString();
        }
        else if (jsonNode.ContainsKey(propertyName: "message"))
        {
            Title = jsonNode[propertyName: "message"]?.ToString();
        }

        if (jsonNode.ContainsKey(propertyName: nameof(Detail).ToLower()))
        {
            Detail = jsonNode[propertyName: nameof(Detail).ToLower()]?.ToString();
        }

        if (jsonNode.ContainsKey(propertyName: nameof(Instance).ToLower()))
        {
            Instance = jsonNode[propertyName: nameof(Instance).ToLower()]?.ToString();
        }

        if (jsonNode.ContainsKey(propertyName: nameof(Errors).ToLower()))
        {
            try
            {
                var arr = jsonNode[propertyName: nameof(Errors).ToLower()]?.AsObject().ToJsonString();
                var errors = s_jsonSerializer.Deserialize<IDictionary<string, string[]>>(json: arr);

                foreach (var error in errors)
                {
                    Errors.Add(item: new ApiErrorDetail
                    {
                        Field = error.Key, Message = error.Value.ToList()
                    });
                }
            }
            catch (Exception)
            {
                //ignore
            }
        }
    }

    public string Title { get; set; }
    public string Instance { get; set; }
    public string Detail { get; set; }
    public List<ApiErrorDetail> Errors { get; private set; } = new();

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(name: nameof(Title), value: Title);
        info.AddValue(name: nameof(Instance), value: Instance);
        info.AddValue(name: nameof(Detail), value: Detail);
        info.AddValue(name: nameof(Errors), value: Errors);
    }
}
