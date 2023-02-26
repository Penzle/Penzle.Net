// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core.Models.Filters;

namespace Penzle.Core.Tests.Filters
{
    public class PageSizeTests
    {
        [Fact]
        public void PageSizeFilter_Should_Set_PageSize_Correctly()
        {
            // Arrange
            const int pageSize = 10;

            // Act
            var pageSizeFilter = new PageSizeFilter(pageSize);

            // Assert
            Assert.Equal(pageSize, pageSizeFilter.PageSize);
        }

        [Fact]
        public void PageSizeFilter_Should_Create_Correct_Rql_Query()
        {
            // Arrange
            const int pageSize = 10;
            var pageSizeFilter = new PageSizeFilter(pageSize);

            // Act
            var result = pageSizeFilter.GetParameter();

            // Assert
            Assert.Equal("filter[PageSize]=10", result);
        }
    }
}
