// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using Penzle.Core.Attributes;

namespace Penzle.Core.Utilities
{
    /// <summary>
    /// A collection of extension methods for the PropertyInfo class.
    /// </summary>
    public static class PropertyExtensions
    {
        /// <summary>
        /// Get the FieldName attribute associated with a property, if it exists.
        /// </summary>
        /// <param name="propertyInfo">The PropertyInfo object representing the property</param>
        /// <returns>The FieldName attribute associated with the property, if it exists, or null if it does not.</returns>
        public static FieldName? GetField([NotNull] this PropertyInfo propertyInfo)
        {
            return propertyInfo
                .GetCustomAttributes(true)
                .FirstOrDefault(x => x.GetType().Equals(typeof(FieldName))) as FieldName;
        }

        /// <summary>
        /// Get field name from the property.
        /// </summary>
        /// <param name="propertyInfo">The PropertyInfo object representing the property</param>
        /// <returns>The name of the field associated with the property, with an optional "system." prefix if the property is declared in a class of type BaseSystem.</returns>
        public static string GetFieldName([NotNull] this PropertyInfo propertyInfo)
        {
            var field = propertyInfo.GetField();
            var filedName = field == null ? propertyInfo.Name : field.Name;
            return propertyInfo.DeclaringType == typeof(BaseSystem) ? $"system.{filedName}" : filedName;
        }
    }

    /// <summary>
    /// A collection of extension methods for working with method calls.
    /// </summary>
    public static class MethodExtensions
    {
        /// Determines if an object contains any of the specified values.
        /// </summary>
        /// <typeparam name="T">The type of the values to check for.</typeparam>
        /// <param name="source">The object to check.</param>
        /// <param name="values">The values to check for.</param>
        /// <returns>true if the object contains any of the specified values, false otherwise.</returns>
        public static bool Contains<T>(this object source, IEnumerable<T> values) => default;
    }
}
