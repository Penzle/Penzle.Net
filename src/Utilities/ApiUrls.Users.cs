namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetUsers(string keyword, int page, int pageSize)
    {
        return "users?page={0}&pageSize={1}&keyword={2}"
            .FormatUri(page, pageSize, keyword);
    }

    internal static Uri GetUser(Guid id)
    {
        return "users/{0}".FormatUri(id);
    }
}
