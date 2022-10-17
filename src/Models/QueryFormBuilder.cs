namespace Penzle.Core.Models;

public sealed class QueryFormBuilder
{
    public QueryFormBuilder()
    {
        Pagination = QueryPaginationBuilder.Default;
    }

    public static QueryEntryBuilder Instance => new();

    private QueryPaginationBuilder Pagination { get; set; }
    private string Language { get; set; }

    public QueryFormBuilder WithLanguage(string language)
    {
        Language = language;
        return this;
    }

    public QueryFormBuilder WithPagination(QueryPaginationBuilder pagination)
    {
        Pagination = pagination ?? QueryPaginationBuilder.Default;
        return this;
    }
}