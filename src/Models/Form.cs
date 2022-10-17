using System;

namespace Penzle.Core.Models;

public class Form<TEntity> : BaseModel<FormSystem> where TEntity : new()
{
    public TEntity Fields { get; set; } = new();

    public override object this[string template, string key] => throw new NotSupportedException(message: "This method is not available for forms.");
}