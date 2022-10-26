using Penzle.Core.Http;
using Penzle.Core.Http.Internal;
using Penzle.Core.Models;

namespace Penzle.Core.Utilities;

public static class BaseExtensions
{
    public static TEntity BaseEntityTo<TEntity>(this IEnumerable<BaseTemplates> @base, string template)
    {
        @base ??= new List<BaseTemplates>();
        IJsonSerializer serializer = new MicrosoftJsonSerializer();
        var json = @base.FirstOrDefault(predicate: baseTemplatesModel => string.Equals(a: baseTemplatesModel.Template, b: template, comparisonType: StringComparison.CurrentCultureIgnoreCase));
        return json != null ? serializer.Deserialize<TEntity>(json: serializer.Serialize(item: json.Fields)) : default;
    }

    public static TEntity BaseEntityTo<TEntity>(this IEnumerable<BaseTemplates> @base)
    {
        return BaseEntityTo<TEntity>(@base: @base, template: typeof(TEntity).Name.ToLower());
    }
}