// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Collections;
using System.Linq.Expressions;

namespace Penzle.Core.Models.Filters
{
    internal class WhereExpression : IQueryParameter
    {
        private readonly string RqlFromat = "filter[where][and][{0}]";

        private static readonly Dictionary<ExpressionType, string> OperatorMap = new()
        {
            [ExpressionType.Equal] = "=",
            [ExpressionType.NotEqual] = "[ne]=",
            [ExpressionType.GreaterThan] = "[gt]=",
            [ExpressionType.GreaterThanOrEqual] = "[ge]=",
            [ExpressionType.LessThan] = "[lt]=",
            [ExpressionType.LessThanOrEqual] = "[le]=",
            [ExpressionType.AndAlso] = "&",
            [ExpressionType.OrElse] = "&",
            [ExpressionType.Not] = "NOT",
            [ExpressionType.Call] = "[inq]="
        };

        private readonly Expression _expression;

        public WhereExpression(Expression expression)
        {
            Guard.ArgumentNotNull(value: expression, name: nameof(expression));
            
            _expression = expression;
        }

        public string GetParameter() => Visit(_expression, isUnary: true).Value;

        private WhereExpressionValue Visit(Expression expression, bool isUnary = false, string prefix = null, string postfix = null)
            => expression.NodeType switch
            {
                ExpressionType.AndAlso or
                ExpressionType.ArrayIndex or
                ExpressionType.Assign or
                ExpressionType.Equal or
                ExpressionType.GreaterThan or
                ExpressionType.GreaterThanOrEqual or
                ExpressionType.LessThan or
                ExpressionType.LessThanOrEqual or
                ExpressionType.NotEqual or
                ExpressionType.OrElse or
                ExpressionType.Coalesce or
                ExpressionType.Add or
                ExpressionType.Subtract or
                ExpressionType.Multiply or
                ExpressionType.Divide or
                ExpressionType.Modulo or
                ExpressionType.And or
                ExpressionType.Or or
                ExpressionType.ExclusiveOr => VisitBinary((BinaryExpression)expression, isUnary, prefix, postfix),

                ExpressionType.Constant => VisitConstant((ConstantExpression)expression, isUnary, prefix, postfix),

                ExpressionType.MemberAccess => VisitMember((MemberExpression)expression, isUnary, prefix, postfix),

                ExpressionType.Call => VisitMethod((MethodCallExpression)expression, isUnary, prefix, postfix),

                _ => UnhandledExpressionType(expression)
            };

        private WhereExpressionValue UnhandledExpressionType(Expression expression) => WhereExpressionValue.New;

        private WhereExpressionValue VisitUnary(UnaryExpression methodCallExpression, bool isUnary, ref int i)
            => WhereExpressionValue.Concat(OperatorMap.GetValueOrDefault(methodCallExpression.NodeType), Visit(methodCallExpression.Operand, true));


        private WhereExpressionValue VisitMethod(MethodCallExpression methodCall, bool isUnary, string prefix, string postfix)
               => methodCall.Method.Name switch
               {
                   "Contains" when methodCall.Method.DeclaringType == typeof(string) =>
                       WhereExpressionValue.Concat(Visit(methodCall.Object), OperatorMap.GetValueOrDefault(ExpressionType.Equal), Visit(methodCall.Arguments[0], prefix: "*", postfix: "*")),

                   "StartsWith" when methodCall.Method.DeclaringType == typeof(string) =>
                       WhereExpressionValue.Concat(Visit(methodCall.Object), OperatorMap.GetValueOrDefault(ExpressionType.Equal), Visit(methodCall.Arguments[0], prefix: "^")),

                   "EndsWith" when methodCall.Method.DeclaringType == typeof(string) =>
                       WhereExpressionValue.Concat(Visit(methodCall.Object), OperatorMap.GetValueOrDefault(ExpressionType.Equal), Visit(methodCall.Arguments[0], prefix: "*", postfix: "$")),

                   "Contains" when methodCall.Arguments.Count == 2 && typeof(IEnumerable).IsAssignableFrom(methodCall.Arguments[1].Type) =>
                   WhereExpressionValue.Concat(
                           Visit(methodCall.Arguments[0]),
                           OperatorMap.GetValueOrDefault(ExpressionType.Call),
                           WhereExpressionValue.IsCollection(values: (IEnumerable)GetValue(methodCall.Arguments[1]))),

                   _ => throw new Exception($"Unsupported method call '{methodCall.Method.Name}'")
               };

        private WhereExpressionValue VisitBinary(BinaryExpression binaryExpression, bool isUnary, string prefix, string postfix)
        {
            var expressionOperator = OperatorMap.GetValueOrDefault(binaryExpression.NodeType);
            var expressionLeft = Visit(binaryExpression.Left);
            var expressionRight = Visit(binaryExpression.Right);


            if (binaryExpression.NodeType == ExpressionType.OrElse)
            {
                return WhereExpressionValue.ConcatOr(expressionLeft, expressionOperator, expressionRight);
            }

            return WhereExpressionValue.Concat(expressionLeft, expressionOperator, expressionRight);
        }

        private WhereExpressionValue VisitConstant(ConstantExpression constantExpression, bool isUnary, string prefix, string postfix)
        {
            var value = constantExpression.Value;

            if (value is int intValue)
            {
                return WhereExpressionValue.Create(intValue.ToString());
            }

            if (value is null)
            {
                return WhereExpressionValue.Create("NULL");
            }

            if (value is string strValue)
            {
                if (strValue == string.Empty)
                {
                    value = $"{prefix}''{postfix}";
                }
                else
                {
                    value = $"{prefix}{strValue}{postfix}";
                }
            }

            if (value is bool boolValue && isUnary)
            {
                return WhereExpressionValue.Concat(WhereExpressionValue.Create(boolValue.ToString()), OperatorMap.GetValueOrDefault(ExpressionType.Equal), WhereExpressionValue.Create("true"));
            }

            return WhereExpressionValue.Create(value.ToString());
        }

        private WhereExpressionValue VisitMember(MemberExpression memberExpression, bool isUnary, string prefix, string postfix)
        {
            if (memberExpression.Member is PropertyInfo property)
            {
                var propertyName = string.Format(RqlFromat, property.GetFieldName());

                if (isUnary && memberExpression.Type == typeof(bool))
                {
                    return WhereExpressionValue.Concat(Visit(memberExpression), OperatorMap.GetValueOrDefault(ExpressionType.Equal), WhereExpressionValue.Create("true"));
                }

                return WhereExpressionValue.Create(propertyName);
            }

            if (memberExpression.Member is FieldInfo field)
            {
                var value = GetValue(memberExpression);

                if (value is string strValue)
                {
                    value = $"{prefix}{strValue}{postfix}";
                }

                return WhereExpressionValue.Create(value.ToString());
            }

            throw new Exception($"Expression does not refer to a property or field '{memberExpression}'.");
        }

        private static object GetValue(Expression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getterValue = getterLambda.Compile();

            return getterValue();
        }
    }
}
