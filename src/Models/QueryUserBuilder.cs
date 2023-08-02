namespace Penzle.Core.Models;

/// <summary>
///     A builder class to facilitate creation of user queries for the API.
/// </summary>
public sealed class QueryUserBuilder
{
    /// <summary>
    ///     Constructs a new QueryUserBuilder with default pagination.
    /// </summary>
    public QueryUserBuilder()
    {
        Pagination = QueryPaginationBuilder.Default;
    }

    /// <summary>
    ///     Gets a new instance of the QueryUserBuilder.
    /// </summary>
    public static QueryUserBuilder Instance => new();

    /// <summary>
    ///     Pagination configuration for the query.
    /// </summary>
    internal QueryPaginationBuilder Pagination { get; set; }

    /// <summary>
    ///     Search keyword to be used in the query.
    /// </summary>
    internal string Keyword { get; set; }

    /// <summary>
    ///     Sets the search keyword for the query.
    /// </summary>
    /// <param name="keyword">The keyword to be used for searching.</param>
    /// <returns>The current QueryUserBuilder instance.</returns>
    public QueryUserBuilder WithKeyword(string keyword)
    {
        Keyword = keyword;
        return this;
    }

    /// <summary>
    ///     Sets the pagination configuration for the query.
    /// </summary>
    /// <param name="pagination">The pagination configuration to be used.</param>
    /// <returns>The current QueryUserBuilder instance.</returns>
    public QueryUserBuilder WithPagination(QueryPaginationBuilder pagination)
    {
        Pagination = pagination ?? QueryPaginationBuilder.Default;
        return this;
    }

    /// <summary>
    ///     Builds the query string to be used in the API call.
    /// </summary>
    /// <returns>The built query string.</returns>
    public string Build()
    {
        return $"keyword={Keyword}&page={Pagination.Page}&pageSize={Pagination.PageSize}";
    }
}
