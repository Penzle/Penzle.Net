namespace Penzle.Core.Models;

public sealed class QueryEntryBuilder
{
    public QueryEntryBuilder()
    {
        Pagination = QueryPaginationBuilder.Default;
    }

    public static QueryEntryBuilder Instance => new();

    internal string Ids { get; set; }
    internal QueryPaginationBuilder Pagination { get; set; }
    internal Guid? ParentId { get; set; } = Constants.EntryRootId;
    internal string Language { get; set; }

    public QueryEntryBuilder WithParentId(Guid parentId)
    {
        ParentId = parentId;
        return this;
    }

    public QueryEntryBuilder WithIds(string ids)
    {
        Ids = ids;
        return this;
    }

    public QueryEntryBuilder WithLanguage(string language)
    {
        Language = language;
        return this;
    }

    public QueryEntryBuilder WithPagination(QueryPaginationBuilder pagination)
    {
        Pagination = pagination ?? QueryPaginationBuilder.Default;
        return this;
    }
}