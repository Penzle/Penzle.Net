namespace Penzle.Core.Models;

public class BaseTemplates
{
    public string Template { get; set; }
    public IDictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}