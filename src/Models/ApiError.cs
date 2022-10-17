using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Penzle.Core.Http;
using Penzle.Core.Http.Internal;

namespace Penzle.Core.Models;

[Serializable]
public class ApiError
{
    private static readonly IJsonSerializer JsonSerializer = new MicrosoftJsonSerializer();

    public ApiError()
    {
    }

    public ApiError(string title)
    {
        Title = title;
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
                var errors = JsonSerializer.Deserialize<IDictionary<string, string[]>>(json: arr);

                foreach (var error in errors)
                {
                    Errors.Add(item: new ApiErrorDetail { Field = error.Key, Message = error.Value.ToList() });
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
}