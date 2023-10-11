// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.
namespace Penzle.Core.Models.Filters
{
    public class IncludeFilter : IQueryParameter
    {
        private readonly int _depthLevel;
        public IncludeFilter(int depthLevel)
        {
            _depthLevel = depthLevel;
        }

        /// <summary>
        /// Returns the preview query parameter as a string.
        /// </summary>
        /// <returns>The encapsulated query parameter.</returns>
        public string GetParameter() => $"include={_depthLevel}";
    }
}
