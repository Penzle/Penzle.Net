namespace Penzle.Core.Models;

public abstract class BaseModel<TBaseSystemModel> where TBaseSystemModel : BaseSystem
{
    public TBaseSystemModel System { get; set; }
    public IEnumerable<BaseTemplates> Base { get; } = new List<BaseTemplates>();

    public virtual object this[string template, string key] => Find(template: template, key: key);

    private object Find(string template, string key)
    {
        return Base.Where(predicate: baseTemplatesModel => string.Equals(a: baseTemplatesModel.Template, b: template, comparisonType: StringComparison.CurrentCultureIgnoreCase)).Select(selector: baseTemplatesModel => baseTemplatesModel.Fields[key: key]).FirstOrDefault();
    }
}