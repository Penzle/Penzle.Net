// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Penzle.Core.Models.Filters;

namespace Penzle.Core.Tests.Filters
{
    public class SelectExpressionTests
    {
        [Fact]
        public void GetParameter_WithMemberInitExpression_ReturnsRqlFormatString()
        {
            // Arrange
            var expression = Expression.MemberInit(Expression.New(typeof(Article)),
                Expression.Bind(typeof(Article).GetProperty("Title"), Expression.Constant("value1")),
                Expression.Bind(typeof(Article).GetProperty("Banner"), Expression.Constant("value2")));
            var select = new SelectExpression(expression);

            // Act
            var result = select.GetParameter();

            // Assert
            Assert.Equal("filter[fields][Title]=true&filter[fields][Banner]=true", result);
        }

        [Fact]
        public void GetParameter_WithUnsupportedExpression_ThrowsArgumentException()
        {
            // Arrange
            var expression = Expression.Constant(new Article());
            var select = new SelectExpression(expression);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => select.GetParameter());
        }

        [Fact]
        public void GetPropertyInfo_WithPropertyInfo_ReturnsSamePropertyInfo()
        {
            // Arrange
            var property = typeof(Article).GetProperty("Title");
            var testInstance = new Article { Title = "test" };
            var select = new SelectExpression(Expression.Constant(testInstance));

            // Act
            var result = select.GetPropertyInfo(property);

            // Assert
            Assert.Equal(property, result);
        }

        [Fact]
        public void GetPropertyInfo_WithMember_ReturnsPropertyInfo()
        {
            // Arrange
            var member = typeof(Article).GetMember("Title")[0];
            var testInstance = new Article { Title = "test" };
            var select = new SelectExpression(Expression.Constant(testInstance));

            // Act
            var result = select.GetPropertyInfo(member);

            // Assert
            Assert.Equal(typeof(Article).GetProperty("Title"), result);
        }
    }
}
