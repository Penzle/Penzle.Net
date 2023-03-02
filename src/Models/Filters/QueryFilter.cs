// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Models.Filters
{
    /// <summary>
    /// A class that encapsulates a query parameter for an HTTP request.
    /// </summary>
    public class QueryFilter : IQueryParameter
    {
        private readonly string _query;

        /// <summary>
        /// Initializes a new instance of the QueryFilter class with the specified query parameter.
        /// </summary>
        /// <param name="query">The query parameter to encapsulate in Penzle RQL format. Example: filter[where][and][Äge]=12</param>
        public QueryFilter(string query)
        {
            _query = query;
        }

        /// <summary>
        /// Returns the encapsulated query parameter as a string.
        /// </summary>
        /// <returns>The encapsulated query parameter.</returns>
        public string GetParameter() => _query;
    }
}
