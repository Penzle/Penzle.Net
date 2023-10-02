using Penzle.Core.Models.Filters;
using System.Linq.Expressions;

namespace Penzle.Core.Models;

public sealed class QueryEntryBuilder
{
    /// <summary>
    /// A collection of query parameters to include in the query entry.
    /// </summary>
    public readonly ICollection<IQueryParameter> QueryParameters;

    /// <summary>
    ///     Initializes a new instance of the QueryEntryBuilder class.
    /// </summary>
    public QueryEntryBuilder()
    {
        QueryParameters = new List<IQueryParameter>();
    }

    public static QueryEntryBuilder New => new();

    public QueryEntryBuilder UsePreviewMode()
    {
        QueryParameters.Add(new PreviewModeFilter());
        return this;
    }

    public QueryEntryBuilder WithLanguage(string language)
    {
        QueryParameters.Add(new LanguageFilter(language, false));
        return this;
    }

    /// <summary>
    /// Builds the query entry as a string.
    /// </summary>
    /// <returns>A string representing the query entry.</returns>
    public string Build()
    {
        return string.Join("&", QueryParameters.Select(x => x.GetParameter()));
    }
}

/// <summary>
/// A builder class for creating query entries for a data source.
/// </summary>
/// <typeparam name="TSource">The type of the data source.</typeparam>
public sealed class QueryEntryBuilder<TSource>
{
    /// <summary>
    /// A collection of query parameters to include in the query entry.
    /// </summary>
    public readonly ICollection<IQueryParameter> QueryParameters;

    /// <summary>
    /// Initializes a new instance of the QueryEntryBuilder class.
    /// </summary>
    public QueryEntryBuilder()
    {
        QueryParameters = new List<IQueryParameter>();
    }

    /// <summary>
    /// Returns a new instance of the QueryEntryBuilder class.
    /// </summary>
    public static QueryEntryBuilder<TSource> New => new();

    /// <summary>
    /// Adds a WHERE clause to the query entry.
    /// </summary>
    /// <param name="predicate">The predicate to use in the WHERE clause.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> Where(Expression<Func<TSource, bool>> predicate)
    {
        QueryParameters.Add(new WhereExpression(predicate.Body));
        return this;
    }

    /// <summary>
    /// Adds a SELECT clause to the query entry.
    /// </summary>
    /// <typeparam name="TResult">The type of the result to select.</typeparam>
    /// <param name="selector">The selector to use in the SELECT clause.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> Select<TResult>(Expression<Func<TSource, TResult>> selector)
    {
        QueryParameters.Add(new SelectExpression(selector.Body));
        return this;
    }

    /// <summary>
    /// Adds an ORDER BY clause to the query entry.
    /// </summary>
    /// <typeparam name="TResult">The type of the key to order by.</typeparam>
    /// <param name="keySelector">The key selector to use in the ORDER BY clause.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> OrderBy<TResult>(Expression<Func<TSource, TResult>> keySelector)
    {
        QueryParameters.Add(new OrderByExpression(keySelector.Body, true));
        return this;
    }

    /// <summary>
    /// Adds a DESCENDING ORDER BY clause to the query entry.
    /// </summary>
    /// <typeparam name="TResult">The type of the key to order by.</typeparam>
    /// <param name="keySelector">The key selector to use in the ORDER BY clause.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> OrderByDescending<TResult>(Expression<Func<TSource, TResult>> keySelector)
    {
        QueryParameters.Add(new OrderByExpression(keySelector.Body, false));
        return this;
    }

    /// <summary>
    /// Adds a paging filter to the query entry.
    /// </summary>
    /// <param name="page">The page number to include in the paging filter.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> Page(int page)
    {
        QueryParameters.Add(new PageFilter(page));
        return this;
    }

    /// <summary>
    /// Adds a page size filter to the query entry.
    /// </summary>
    /// <param name="pageSize">The page size to include in the page size filter.</param>
    /// <returns>The updated QueryEntryBuilder instance.</returns>
    public QueryEntryBuilder<TSource> PageSize(int pageSize)
    {
        QueryParameters.Add(new PageSizeFilter(pageSize));
        return this;
    }

    public QueryEntryBuilder<TSource> UsePreviewMode()
    {
        QueryParameters.Add(new PreviewModeFilter());
        return this;
    }

    public QueryEntryBuilder<TSource> WithLanguage(string language)
    {
        QueryParameters.Add(new LanguageFilter(language, true));
        return this;
    }

    /// <summary>
    /// Builds the query entry as a string.
    /// </summary>
    /// <returns>A string representing the query entry.</returns>
    public string Build()
    {
        return string.Join("&", QueryParameters.Select(x => x.GetParameter()));
    }
}
