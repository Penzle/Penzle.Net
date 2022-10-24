namespace Penzle.Core.Models;

public class CreateEntryRequest<TEntity> where TEntity : new()
{
    public string UrlAlias { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }
    public string Template { get; set; }
    public Guid? ParentId { get; set; }
    public List<BaseTemplates> Base { get; set; } = new();
    public TEntity Fields { get; set; } = new();
}