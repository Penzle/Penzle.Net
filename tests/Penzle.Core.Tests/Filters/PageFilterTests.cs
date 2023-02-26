// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Penzle.Core.Models.Filters;

namespace Penzle.Core.Tests.Filters
{
    public class PageFilterTests
    {
        [Fact]
        public void PageFilter_Should_Set_Page_To_Zero_For_Page_Less_Than_Or_Equal_To_One()
        {
            // Arrange
            const int page = 1;

            // Act
            var pageFilter = new PageFilter(page);

            // Assert
            Assert.Equal(0, pageFilter.Page);
        }

        [Fact]
        public void PageFilter_Should_Decrement_Page_By_One()
        {
            // Arrange
            const int page = 3;

            // Act
            var pageFilter = new PageFilter(page);

            // Assert
            Assert.Equal(2, pageFilter.Page);
        }

        [Fact]
        public void PageFilter_Should_Create_Correct_Rql_Query()
        {
            // Arrange
            const int page = 3;
            var pageFilter = new PageFilter(page);

            // Act
            var result = pageFilter.GetParameter();

            // Assert
            Assert.Equal("filter[page]=2", result);
        }
    }
}
