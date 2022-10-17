namespace Penzle.Core.Models;

public abstract class Entry<TEntity> : BaseModel<EntrySystem> where TEntity : new()
{
    public TEntity Fields { get; set; } = new();
}