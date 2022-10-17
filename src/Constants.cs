using System;

namespace Penzle.Core;

internal static class Constants
{
    internal static Guid EntryRootId = new(g: "1088ea99-1411-4fb9-b694-3c974544b3aa");
    internal static Guid AssetRootId = new(g: "f56e30b8-c9bf-4bed-864d-f590dc727089");
    internal static readonly string AddressTemplate = "{0}api/project/{1}/environment/{2}/";
}