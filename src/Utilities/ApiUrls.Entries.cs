namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetEntries<TEntry>(string template, QueryEntryBuilder<TEntry> queryEntryBuilder)
    {
        if (queryEntryBuilder.QueryParameters.Any())
        {
            return "entries/{0}?{1}".FormatUri(template, queryEntryBuilder.Build());
        }

        return "entries/{0}".FormatUri(template);
    }

    internal static Uri GetEntry(Guid entryId, string language)
    {
        return !string.IsNullOrWhiteSpace(value: language) ? "entries/{0}?language={1}".FormatUri(entryId, language) : "entries/{0}".FormatUri(entryId);
    }

    internal static Uri GetEntryByAliasUrl(string uri, string language)
    {
        return !string.IsNullOrWhiteSpace(value: language) ? "entries/url?language={0}&aliasUrl={1}".FormatUri(language, uri) : "entries?aliasUrl={0}".FormatUri(uri);
    }

    public static Uri CreateEntry()
    {
        return "entries".FormatUri();
    }

    public static Uri UpdateEntry(Guid entryId)
    {
        return "entries/{0}".FormatUri(entryId);
    }

    public static Uri DeleteEntry(Guid entryId)
    {
        return "entries/{0}".FormatUri(entryId);
    }
}
