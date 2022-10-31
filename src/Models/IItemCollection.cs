namespace Penzle.Core.Models;

/// <summary>
///     Reflects a collection of data returned from an API.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IItemCollection<TEntity>
{
    TEntity[] Items { get; set; }
}
