namespace Penzle.Core.Models;

public class Entry<TEntity> : BaseModel<EntrySystem> where TEntity : new()
{
    public TEntity Fields { get; set; } = new();
}