// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Models;

public class ArticleWithSystem : Article
{
    [JsonPropertyName("system")] public EntrySystem? System { get; init; }
}
