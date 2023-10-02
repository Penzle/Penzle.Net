// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.
namespace Penzle.Core.Models.Filters
{
    public class PreviewModeFilter : IQueryParameter
    {
        /// <summary>
        /// Returns the preview query parameter as a string.
        /// </summary>
        /// <returns>The encapsulated query parameter.</returns>
        public string GetParameter() => "preview=true";
    }
}
