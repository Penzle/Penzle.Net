using System.Reflection;

namespace Penzle.Core.Utilities;

internal static partial class Extensions
{
    internal static IEnumerable<PropertyInfo> GetProperties(this object @this)
    {
        return @this.GetType().GetProperties();
    }
}
