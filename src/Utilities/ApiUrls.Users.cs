// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

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

    internal static Uri GetUserCustomData(Guid id)
    {
        return "{0}users/{1}/custom-data".FormatUri(Constants.IgnoreProjectKeyword, id);
    }

    internal static Uri EnrollUser()
    {
        return "{0}users".FormatUri(Constants.IgnoreProjectKeyword);
    }

    internal static Uri UpdateUserCustomData(Guid id)
    {
        return "{0}users/{1}/custom-data".FormatUri(Constants.IgnoreProjectKeyword, id);
    }
}
