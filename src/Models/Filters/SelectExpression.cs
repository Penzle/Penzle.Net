// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Penzle.Core.Models.Filters
{
    internal class SelectExpression : IQueryParameter
    {
        private readonly string RqlFromat = "filter[fields][{0}]=true";

        public SelectExpression([NotNull] Expression expression)
        {
            Guard.ArgumentNotNull(value: expression, name: nameof(expression));

            Expression = expression;
        }

        public Expression Expression { get; }

        public string GetParameter()
        {
            var selectedFields = GetFields();
            
            return string.Join("&", selectedFields.Select(x => string.Format(RqlFromat, x)));
        }

        private List<string> GetFields()
        {
            var selectedFields = new List<string>();

            switch (Expression)
            {
                case NewExpression newExpression:
                    foreach (var member in newExpression.Members)
                    {
                        var property = GetPropertyInfo(member);
                        selectedFields.Add(property.GetFieldName());
                    }
                    break;

                case MemberInitExpression memberInitExpression:
                    foreach (var binding in memberInitExpression.Bindings)
                    {
                        if (binding is MemberAssignment memberAssignment)
                        {
                            var property = GetPropertyInfo(memberAssignment.Member);
                            selectedFields.Add(property.GetFieldName());
                        }
                    }
                    break;

                default:
                    throw new ArgumentException($"Invalid expression type: {Expression.GetType()}");
            }

            return selectedFields;
        }

        public PropertyInfo GetPropertyInfo(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case PropertyInfo propertyInfo:
                    return propertyInfo;

                case MemberInfo member:
                    var type = member.DeclaringType;
                    return type.GetProperty(member.Name);

                default:
                    throw new ArgumentException($"Invalid member type: {memberInfo.GetType()}");
            }
        }
    }
}
