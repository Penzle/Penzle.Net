// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.
namespace Penzle.Core.Attributes
{
    /// <summary>
    /// A custom attribute that can be applied to a property to specify the name of the corresponding field from the Penzle data template.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldName : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the FieldName class with the specified field name.
        /// </summary>
        /// <param name="name">The name of the corresponding field.</param>
        public FieldName(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the corresponding field.
        /// </summary>
        public string Name { get; private set; }
    }
}
