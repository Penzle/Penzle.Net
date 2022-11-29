// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Net;
using Penzle.Core;
using Penzle.Core.Models;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples;

internal record AssetExamples
{
    public async static Task ExampleHowToGetAssetCollection(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var query = QueryAssetBuilder.Instance;
        var assetCollection = await deliveryPenzleClient.Asset.GetAssets(query: query, cancellationToken: CancellationToken.None);

        // Print the response of asset collection which is served in pagination list to the console.
        Console.WriteLine(value: assetCollection.HasNextPage); //Returns true if the superset is not empty and PageNumber is less than or equal to PageCount and this is not the last subset within the superset.
        Console.WriteLine(value: assetCollection.HasPreviousPage); //Returns true if the superset is not empty and PageNumber is greater than or equal to 1 and this is not the first subset within the superset.
        Console.WriteLine(value: assetCollection.PageIndex); //Returns the current page number.
        Console.WriteLine(value: assetCollection.PageSize); //Returns the number of items in the current page.
        Console.WriteLine(value: assetCollection.TotalCount); //Returns the total number of items in the superset.
        Console.WriteLine(value: assetCollection.TotalPages); //Returns the total number of pages in the superset.
        Console.WriteLine(value: assetCollection.Items); // Returns the collection of Penzle.Core.Model.Asset in the current page.
    }

    public async static Task ExampleHowToGetAsset(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var assetId = new Guid(g: "F078FC7D-C3E6-459F-AD21-D34F71E6195B");
        var asset = await deliveryPenzleClient.Asset.GetAsset(id: assetId, cancellationToken: CancellationToken.None);

        // Print the response of returned asset to the console.
        Console.WriteLine(value: asset.Id); // Returns the unique identifier of the asset.
        Console.WriteLine(value: asset.Name); // Returns the name of the asset.
        Console.WriteLine(value: asset.Description); // Returns the description of the asset.
        Console.WriteLine(value: asset.Tags); // Returns the tags of the asset and it can be enumerated.
        Console.WriteLine(value: asset.CreatedAt); // Returns the date and time when the asset was created.
        Console.WriteLine(value: asset.ModifiedAt); // Returns the date and time when the asset was last updated.
        Console.WriteLine(value: asset.AssetMimeType); // Returns the mime type of the asset. This is complex object and be access to own properties.
        Console.WriteLine(value: asset.Size); // Returns the content length size of the asset.
        Console.WriteLine(value: asset.Url); // Returns the url from CDN of the asset.
        Console.WriteLine(value: asset.Type); // Returns the type of the asset it can be folder or file.
    }

    public async static Task ExampleHowToCreateAsset(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var asset = new AddAssetRequest
        {
            FolderId = new Guid("B140B0E8-C1CB-4E98-95A1-74EE8D9437E5"), // Represents the unique identifier of the parent folder. In case if this value is null then the asset will be created against to root folder.
            Description = "This is a description of the asset.", // Represents the description of the asset.
            Language = "en-US" // Represents the language of the asset.
        };

        await using (var file = new FileStream(path: "Penzle.png", mode: FileMode.Open, access: FileAccess.Read)) // Load file stream from disk.
        {
            asset!.Payload = file; // Represents the file stream of the asset.
        }

        // Create a new asset.
        var assetId = await managementPenzleClient.Asset.AddAsset(request: asset, cancellationToken: CancellationToken.None);

        // Print the response of returned asset to the console.
        Console.WriteLine(value: assetId); // Returns the unique identifier of the asset.
    }

    public async static Task ExampleHowToUpdateAsset(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var asset = new UpdateAssetRequest
        {
            Id = new Guid("F078FC7D-C3E6-459F-AD21-D34F71E6195B"), // Represents the unique identifier of the asset.
            Description = "This is updated asset description..", // Represents the description of the asset.
            Language = "en-US" // Represents the language of the asset.
        };

        await using (var file = new FileStream(path: "NewPenzleLogo.png", mode: FileMode.Open, access: FileAccess.Read)) // Load file stream from disk.
        {
            asset!.Payload = file; // Represents the file stream of the asset.
        }

        // Update the asset.
        var httpStatusCode = await managementPenzleClient.Asset.UpdateAsset(request: asset, cancellationToken: CancellationToken.None);

        // Print the True if the asset was updated successfully.
        Console.WriteLine(value: httpStatusCode == HttpStatusCode.NoContent);
    }

    public async static Task ExampleHowToDeleteAsset(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var assetId = new Guid("F078FC7D-C3E6-459F-AD21-D34F71E6195B"); // Represents the unique identifier of the asset.

        // Update the asset.
        var httpStatusCode = await managementPenzleClient.Asset.DeleteAsset(id: assetId, cancellationToken: CancellationToken.None);

        // Print the True if the asset was updated successfully.
        Console.WriteLine(value: httpStatusCode == HttpStatusCode.NoContent);
    }

    public async static Task ExampleHowToDeleteAssets(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Represents the unique identifiers of the assets.
        var assetIds = new[]
        {
            new("F078FC7D-C3E6-459F-AD21-D34F71E6195B"), // Represents the unique identifier of the asset.
            new Guid("B140B0E8-C1CB-4E98-95A1-74EE8D9437E5") // Represents the unique identifier of the asset.
        };

        // Update the asset.
        var httpStatusCode = await managementPenzleClient.Asset.DeleteAssets(ids: assetIds, cancellationToken: CancellationToken.None);

        // Print the True if the asset was updated successfully.
        Console.WriteLine(value: httpStatusCode == HttpStatusCode.NoContent);
    }
}
