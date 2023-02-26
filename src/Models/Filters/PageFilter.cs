// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Models.Filters
{
    internal class PageFilter : IQueryParameter
    {
        private readonly string RqlFromat = "filter[page]={0}";
        public PageFilter(int page)
        {
            Page = page switch
            {
                <= 0 or 1 => 0,
                _ => page - 1
            };
        }

        public int Page { get; }

        public string GetParameter()
        {
            return string.Format(RqlFromat, Page);
        }
    }
}
