// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core;
using Penzle.Core.Models;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries
{
    internal class AssetsExamples
    {
        public static async Task ExampleHowToAssets(Uri uri, string apiKey)
        {
            // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
            var managementPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
            {
                options.Project = "main"; // Define the project name which you want to use.
                options.Environment = "default"; // Define the environment name which you want to use for the project.
            });


            var query = QueryAssetBuilder.Instance;
            var assetCollection = await managementPenzleClient.Asset.GetAssets(query: query, cancellationToken: CancellationToken.None);

            // Print the response of asset collection which is served in pagination list to the console.
            Console.WriteLine(value: assetCollection.HasNextPage); //Returns true if the superset is not empty and PageNumber is less than or equal to PageCount and this is not the last subset within the superset.
            Console.WriteLine(value: assetCollection.HasPreviousPage); //Returns true if the superset is not empty and PageNumber is greater than or equal to 1 and this is not the first subset within the superset.
            Console.WriteLine(value: assetCollection.PageIndex); //Returns the current page number.
            Console.WriteLine(value: assetCollection.PageSize); //Returns the number of items in the current page.
            Console.WriteLine(value: assetCollection.TotalCount); //Returns the total number of items in the superset.
            Console.WriteLine(value: assetCollection.TotalPages); //Returns the total number of pages in the superset.
        }
    }
}
