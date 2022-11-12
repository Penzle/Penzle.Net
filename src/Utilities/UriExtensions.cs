namespace Penzle.Core.Utilities;

internal static class UriExtensions
{
    public static Uri StripRelativeUri(this Uri uri)
    {
        return new Uri(baseUri: uri, relativeUri: "/");
    }

    public static Uri ReplaceRelativeUri(this Uri uri, Uri relativeUri)
    {
        return new Uri(baseUri: StripRelativeUri(uri: uri), relativeUri: relativeUri);
    }

    public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
    {
        Guard.ArgumentNotNull(value: uri, name: nameof(uri));

        if (parameters == null || !parameters.Any())
        {
            return uri;
        }

        var p = new Dictionary<string, string>(dictionary: parameters);

        var hasQueryString = uri.OriginalString.IndexOf(value: "?", comparisonType: StringComparison.Ordinal);

        var uriWithoutQuery = hasQueryString == -1 ? uri.ToString() : uri.OriginalString[..hasQueryString];

        var queryString = uri.IsAbsoluteUri switch
        {
            true => uri.Query,
            _ => hasQueryString switch
            {
                -1 => string.Empty,
                _ => uri.OriginalString[hasQueryString..]
            }
        };

        var values = queryString.Replace(oldValue: "?", newValue: "")
            .Split(separator: new[]
            {
                '&'
            }, options: StringSplitOptions.RemoveEmptyEntries);

        var existingParameters = values.ToDictionary(
            keySelector: key => key[..key.IndexOf(value: '=')],
            elementSelector: value => value[(value.IndexOf(value: '=') + 1)..]);

        foreach (var existing in existingParameters.Where(predicate: existing => !p.ContainsKey(key: existing.Key)))
        {
            p.Add(key: existing.Key, value: existing.Value);
        }

        string MapValueFunction(string key, string value)
        {
            return key == "q" ? value : Uri.EscapeDataString(stringToEscape: value);
        }

        var query = string.Join(separator: "&", values: p.Select(selector: kvp => kvp.Key + "=" + MapValueFunction(key: kvp.Key, value: kvp.Value)));
        switch (uri.IsAbsoluteUri)
        {
            case true:
                {
                    var uriBuilder = new UriBuilder(uri: uri)
                    {
                        Query = query
                    };
                    return uriBuilder.Uri;
                }
            default:
                return new Uri(uriString: $"{uriWithoutQuery}?{query}", uriKind: UriKind.Relative);
        }
    }
}
