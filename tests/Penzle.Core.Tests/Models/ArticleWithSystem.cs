// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Models;

public class ArticleWithSystem
{
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("slug")] public string? Slug { get; init; }
    [JsonPropertyName("banner")] public string? Banner { get; init; }

    [JsonPropertyName("thumbnail")] public string? Thumbnail { get; init; }

    [JsonPropertyName("thumbnailLandscape")]
    public string? ThumbnailLandscape { get; init; }

    [JsonPropertyName("description")] public string? Description { get; init; }

    [JsonPropertyName("body")] public string? Body { get; init; }

    [JsonPropertyName("relatedArticle")] public string? RelatedArticle { get; init; }

    [JsonPropertyName("metaTitle")] public string? MetaTitle { get; init; }

    [JsonPropertyName("metaDescription")] public string? MetaDescription { get; init; }

    [JsonPropertyName("system")] public EntrySystem? System { get; set; }
}
