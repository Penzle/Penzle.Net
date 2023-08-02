namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetUsers(string keyword, int page, int pageSize)
    {
        return "{0}users?page={1}&pageSize={2}&keyword={3}"
            .FormatUri(Constants.IgnoreProjectKeyword, page, pageSize, keyword);
    }

    internal static Uri GetUser(Guid id)
    {
        return "{0}users/{1}".FormatUri(Constants.IgnoreProjectKeyword, id);
    }
}
