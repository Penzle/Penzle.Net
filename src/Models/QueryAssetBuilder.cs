namespace Penzle.Core.Models;

public sealed class QueryAssetBuilder
{
    public QueryAssetBuilder()
    {
        Pagination = QueryPaginationBuilder.Default;
    }

    public static QueryAssetBuilder Instance => new();

    internal string Ids { get; set; }
    internal QueryPaginationBuilder Pagination { get; set; }
    internal string Language { get; set; }
    internal Guid ParentId { get; set; }
    internal string Keyword { get; set; }
    internal string Tag { get; set; }
    internal string MimeType { get; set; }

    public QueryAssetBuilder WithMimeType(string mimeType)
    {
        MimeType = mimeType;
        return this;
    }

    public QueryAssetBuilder WithIds(string ids)
    {
        Ids = ids;
        return this;
    }

    public QueryAssetBuilder WithTag(string tag)
    {
        Tag = tag;
        return this;
    }

    public QueryAssetBuilder WithKeyword(string keyword)
    {
        Keyword = keyword;
        return this;
    }

    public QueryAssetBuilder WithParentId(Guid parentId)
    {
        ParentId = parentId;
        return this;
    }

    public QueryAssetBuilder FromRoot()
    {
        ParentId = Constants.AssetRootId;
        return this;
    }

    public QueryAssetBuilder WithLanguage(string language)
    {
        Language = language;
        return this;
    }

    public QueryAssetBuilder WithPagination(QueryPaginationBuilder pagination)
    {
        Pagination = pagination ?? QueryPaginationBuilder.Default;
        return this;
    }
}