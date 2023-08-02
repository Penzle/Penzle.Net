namespace Penzle.Core.Utilities;

internal static class UriExtensions
{
    public static Uri StripRelativeUri(this Uri uri)
    {
        return new Uri(uri, "/");
    }

    public static Uri ReplaceRelativeUri(this Uri uri, Uri relativeUri)
    {
        return new Uri(StripRelativeUri(uri), relativeUri);
    }

    public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
    {
        Guard.ArgumentNotNull(uri, nameof(uri));
        if (parameters == null || !parameters.Any())
        {
            return uri;
        }

        var p = new Dictionary<string, string>(parameters);

        var hasQueryString = uri.OriginalString.IndexOf("?", StringComparison.Ordinal);

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

        var values = queryString.Replace("?", "")
            .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

        var existingParameters = values.ToDictionary(
            key => key[..key.IndexOf('=')],
            value => value[(value.IndexOf('=') + 1)..]);

        foreach (var existing in existingParameters.Where(existing => !p.ContainsKey(existing.Key)))
        {
            p.Add(existing.Key, existing.Value);
        }

        string MapValueFunction(string key, string value)
        {
            return key == "q" ? value : Uri.EscapeDataString(value);
        }

        var query = string.Join("&", p.Select(kvp => kvp.Key + "=" + MapValueFunction(kvp.Key, kvp.Value)));
        switch (uri.IsAbsoluteUri)
        {
            case true:
                {
                    var uriBuilder = new UriBuilder(uri) { Query = query };
                    return uriBuilder.Uri;
                }
            default:
                return new Uri($"{uriWithoutQuery}?{query}", UriKind.Relative);
        }
    }
}
