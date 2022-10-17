using System.Collections.Generic;

namespace Penzle.Core.Models;

public abstract class BaseTemplates
{
    public string Template { get; set; }
    public IDictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}