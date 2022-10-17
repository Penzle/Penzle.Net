using System;

namespace Penzle.Core.Models;

public class CreateFormRequest<TEntity> where TEntity : new()
{
    public string Language { get; set; }
    public string Name { get; set; }
    public string Form { get; set; }
    public Guid? ParentId { get; set; }
    public TEntity Fields { get; set; } = new();
}