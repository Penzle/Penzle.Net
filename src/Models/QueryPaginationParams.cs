namespace Penzle.Core.Models;

public sealed class QueryPaginationBuilder
{
    public static QueryPaginationBuilder Default => new();

    internal int Page { get; set; }
    internal int PageSize { get; set; } = 10;

    public QueryPaginationBuilder WithPage(int page)
    {
        Page = page switch
        {
            <= 0 or 1 => 0,
            _ => page - 1
        };

        return this;
    }

    public QueryPaginationBuilder WithPageSize(int pageSize)
    {
        PageSize = pageSize;
        return this;
    }
}