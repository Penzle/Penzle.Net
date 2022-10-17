using System;
using System.Linq;

namespace Penzle.Core.Utilities;

internal static partial class ApiUrls
{
    internal static Uri GetAssets(Guid parentId, string language, string keyword, string tag, string mimeType, string ids, int page, int pageSize, string orderBy, string direction)
    {
        return "assets?parentId={0}&language={1}&keyword={2}&tag={3}&mimeType={4}&page={5}&pageSize={6}&orderBy={7}&direction={8}&ids={9}"
            .FormatUri(parentId, language, keyword, tag, mimeType, page, pageSize, orderBy, direction, ids);
    }

    internal static Uri GetAsset(Guid id, string language)
    {
        return "assets/{0}?language={1}".FormatUri(id, language);
    }

    internal static Uri AddAsset(Guid folderId, string language)
    {
        return "assets/folder/{0}/file?language={1}".FormatUri(folderId, language);
    }

    internal static Uri UpdateAsset(Guid id, string language)
    {
        return "assets/file/{0}?language={1}".FormatUri(id, language);
    }

    internal static Uri DeleteAssets(params Guid[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            throw new ArgumentNullException(paramName: nameof(ids), message: "Id collection cannot be null or empty.");
        }

        var parameter = string.Join(separator: "&", values: ids.Select(selector: id => $"ids={id}").ToList());
        return "assets?{0}".FormatUri(parameter);
    }
}