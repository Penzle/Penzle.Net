// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Collections;

namespace Penzle.Core.Models.Filters
{
    internal class WhereExpressionValue
    {
        private static readonly string RqlFromat = "filter[where][and]";
        private static readonly string RqlFromatOr = "filter[where][or]";

        public string Value { get; }

        private WhereExpressionValue(string value)
        {
            Value = value;
        }

        public static WhereExpressionValue New => new("");

        public static WhereExpressionValue Create(string value) => new(value);

        public static WhereExpressionValue Concat(string @operator, WhereExpressionValue operand)
            => new($"{@operator}{operand.Value}");

        public static WhereExpressionValue Concat(WhereExpressionValue left, string @operator, WhereExpressionValue right)
            => new($"{left.Value}{@operator}{right.Value}");

        public static WhereExpressionValue ConcatOr(WhereExpressionValue left, string @operator, WhereExpressionValue right)
            => new($"{left.Value}{@operator}{right.Value.Replace(RqlFromat, RqlFromatOr)}");

        public static WhereExpressionValue IsCollection(IEnumerable values)
        {
            var typedValues = values.Cast<object>();
            return new WhereExpressionValue(string.Join(",", typedValues.Select(x => x.ToString())));
        }
    }
}
