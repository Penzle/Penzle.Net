// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Models.Filters
{
    public class LanguageFilter : IQueryParameter
    {
        private readonly string _rqlFromat = "filter[where][and][system.language]={0}";
        private readonly string _queryParameter = "language={0}";
        private readonly string _langaugey;
        private readonly bool _isRQLParametar;

        /// <summary>
        ///     Initializes a new instance of the LanguageFilter class with the specified query parameter.
        /// </summary>
        /// <param name="language">Langauge code.</param>
        /// <param name="isRQLParametar">Is RQL Query</param>
        public LanguageFilter(string language, bool isRQLParametar)
        {
            _langaugey = language;
            _isRQLParametar = isRQLParametar;
        }

        /// <summary>
        /// Returns the encapsulated query parameter as a string.
        /// </summary>
        /// <returns>The encapsulated query parameter.</returns>
        public string GetParameter() => _isRQLParametar ? string.Format(_rqlFromat, _langaugey) : string.Format(_queryParameter, _langaugey);
    }
}
