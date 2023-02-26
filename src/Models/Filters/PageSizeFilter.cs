// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Models.Filters
{
    internal class PageSizeFilter : IQueryParameter
    {
        private readonly string RqlFromat = "filter[PageSize]={0}";
        public PageSizeFilter(int pageSize)
        {
            PageSize = pageSize;
        }

        public int PageSize { get; }

        public string GetParameter()
        {
            return string.Format(RqlFromat, PageSize);
        }
    }
}
