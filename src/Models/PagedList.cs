namespace Penzle.Core.Models;

/// <summary>
///     Reflects a collection of data returned from an API that can be paged.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public sealed class PagedList<TEntity> : IItemCollection<TEntity>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public TEntity[] Items { get; set; }
}
