// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Penzle.Core.Models.Filters;

namespace Penzle.Core.Tests.Filters
{
    public class OrderByExpressionTests
    {
        [Fact]
        public void GetParameter_WithValidMemberExpressionAndIsAscending_ReturnsRqlFormatString()
        {
            // Arrange
            var expression = GetMemberExpression("City");
            var orderBy = new OrderByExpression(expression, true);

            // Act
            var result = orderBy.GetParameter();

            // Assert
            Assert.Equal("filter[order]=City ASC", result);
        }

        [Fact]
        public void GetParameter_WithValidMemberExpressionAndIsDescending_ReturnsRqlFormatString()
        {
            // Arrange
            var expression = GetMemberExpression("City");
            var orderBy = new OrderByExpression(expression, false);

            // Act
            var result = orderBy.GetParameter();

            // Assert
            Assert.Equal("filter[order]=City DESC", result);
        }

        [Fact]
        public void GetParameter_WithNonMemberExpression_ReturnsEmptyString()
        {
            // Arrange
            var expression = Expression.Constant(new object());
            var orderBy = new OrderByExpression(expression, true);

            // Act
            var result = orderBy.GetParameter();

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void VisitMember_WithValidMemberExpression_ReturnsFieldName()
        {
            // Arrange
            var expression = GetMemberExpression("City");
            var orderBy = new OrderByExpression(expression, true);

            // Act
            var result = orderBy.VisitMember(expression);

            // Assert
            Assert.Equal("City", result);
        }

        private MemberExpression GetMemberExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(Address), "x");
            var member = Expression.Property(parameter, propertyName);
            return member;
        }
    }
}
