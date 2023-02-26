// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Penzle.Core.Models.Filters
{
    internal class OrderByExpression : IQueryParameter
    {
        private readonly string RqlFromat = "filter[order]={0}{1}";
        public OrderByExpression([NotNull] Expression expression, bool isAscending)
        {
            Guard.ArgumentNotNull(value: expression, name: nameof(expression));
            
            Expression = expression;
            IsAscending = isAscending;
        }

        public Expression Expression { get; }
        public bool IsAscending { get; }
        private string Order => IsAscending ? " ASC" : " DESC";

        public string GetParameter()
        {
            if (ExpressionType.MemberAccess == Expression?.NodeType)
            {
                return string.Format(RqlFromat, VisitMember((MemberExpression)Expression), Order);
            }
            return string.Empty;
        }

        public string VisitMember(MemberExpression memberExpression)
        {
            if (memberExpression.Member is PropertyInfo property)
            {
                return property.GetFieldName();
            }
            else
            {
                throw new Exception($"Expression does not refer to a property or field '{memberExpression}'.");
            }
        }
    }
}
