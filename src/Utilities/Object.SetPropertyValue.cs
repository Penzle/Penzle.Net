using System.Reflection;

namespace Penzle.Core.Utilities;

internal static partial class Extensions
{
    internal static void SetPropertyValue<T>(this T @this, string propertyName, object value)
    {
        var type = @this.GetType();
        var property = type.GetProperty(name: propertyName, bindingAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        if (property != null)
        {
            property.SetValue(obj: @this, value: value, index: null);
        }
    }
}
