// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Globalization;
using System.Net;
using Penzle.Core;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;
using Penzle.Core.Utilities;
using Penzle.Net.GettingStarted.ConsoleExample.Models;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples;

internal record EntryExamples
{
#pragma warning disable IDE0036 // Order modifiers
    public async static Task ExampleHowToGetEntryCollection(Uri uri, string apiKey)
#pragma warning restore IDE0036 // Order modifiers
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Using query builder to define request parameters.
        var query = QueryEntryBuilder.Instance
            .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
            .WithPagination(
                pagination: QueryPaginationBuilder.Default
                    .WithPage(page: 1)
                    .WithPageSize(pageSize: 10));
        try
        {
            // Using created instance of the Penzle API client, you can call the API methods for fetching the entry data with pagination.
            var entries = await deliveryPenzleClient.Entry.GetPaginationListEntries<Entry<Author>>(query: query);

            // Print the total number of entries.
            Console.WriteLine(value: $"Total count of entries: {entries.TotalCount}");

            // Print the entries to the console.
            foreach (var entry in entries.Items)
            {
                // Print the entry system fields.
                Console.WriteLine(value: $"Entry {entry.System.Name} system fields:");
                Console.WriteLine(value: $"System Id: {entry.System.Id}");
                Console.WriteLine(value: $"System Template: {entry.System.Template}");
                Console.WriteLine(value: $"System Language: {entry.System.Language}");
                Console.WriteLine(value: $"System Version: {entry.System.Version}");
                Console.WriteLine(value: $"System CreatedAt: {entry.System.CreatedAt}");
                Console.WriteLine(value: $"System ModifiedAt: {entry.System.ModifiedAt}");

                // Print the entry fields.
                Console.WriteLine(value: $"Entry {entry.System.Name} fields:");
                Console.WriteLine(value: $"Summary: {entry.Fields.Summary}");

                // Print the entry base collection fields.
                foreach (var @base in entry.Base)
                {
                    Console.WriteLine(value: "Entry base fields:");
                    Console.WriteLine(value: $"Fields dictionary: {@base.Fields}");
                }

                // Get strong type base object.
                var address = entries.Items.First().Base.BaseEntityTo<Address>();

                // Print base address object
                Console.WriteLine(value: address.City);
                Console.WriteLine(value: address.State);
                Console.WriteLine(value: address.Zip);
                Console.WriteLine(value: address.Street);
            }
        }
        catch (PenzleException exception)
        {
            Console.WriteLine(value: exception);
            throw;
        }
    }

    public async static Task ExampleHowToGetEntryCollectionUsingTemplate(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Using query builder to define request parameters.
        var query = QueryEntryBuilder.Instance
            .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
            .WithPagination(
                pagination: QueryPaginationBuilder.Default
                    .WithPage(page: 1)
                    .WithPageSize(pageSize: 10));
        try
        {
            // Using created instance of the Penzle API client, you can call the API methods for fetching the entry data with pagination.
            var entries = await deliveryPenzleClient.Entry.GetPaginationListEntries<Entry<Author>>(template: "author", query: query);

            // Print the total number of entries.
            Console.WriteLine(value: $"Total count of entries: {entries.TotalCount}");
        }
        catch (PenzleException exception)
        {
            Console.WriteLine(value: exception);
            throw;
        }
    }

    public async static Task ExampleHowToGetEntryById(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Define the entry id which you want to get.
        var entryId = new Guid("797D46CD-1283-4A4B-A335-EE46809D5F56");

        // Using created instance of the Penzle API client, you can call the API methods for fetching the entry data.
        var medicalRelease = await deliveryPenzleClient.Entry.GetEntry<MedicalRelease>(entryId: entryId, cancellationToken: CancellationToken.None);

        // Print the entry.
        Console.WriteLine(value: $"First Name: {medicalRelease.FirstName}");
        Console.WriteLine(value: $"Last Name: {medicalRelease.LastName}");
        Console.WriteLine(value: $"Date of Birth: {medicalRelease.DateOfBirth}");
        Console.WriteLine(value: $"Email address: {medicalRelease.EmailAddress}");
        Console.WriteLine(value: $"Sex: {medicalRelease.Sex}");
    }

    public async static Task ExampleHowToGetEntryBySlug(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Define the entry slug which you want to get.
        var slug = "/medial-release/medical-release-1";

        // Using created instance of the Penzle API client, you can call the API methods for fetching the entry data.
        var medicalRelease = await deliveryPenzleClient.Entry.GetEntry<MedicalRelease>(uri: slug, cancellationToken: CancellationToken.None);

        // Print the entry.
        Console.WriteLine(value: $"First Name: {medicalRelease.FirstName}");
        Console.WriteLine(value: $"Last Name: {medicalRelease.LastName}");
        Console.WriteLine(value: $"Date of Birth: {medicalRelease.DateOfBirth}");
        Console.WriteLine(value: $"Email address: {medicalRelease.EmailAddress}");
        Console.WriteLine(value: $"Sex: {medicalRelease.Sex}");
    }

    public async static Task ExampleHowToCreateEntry(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // Create a new instance of the form entry.
        var medicalRelease = new MedicalRelease
        {
            ParentName = "David Smith",
            EmailAddress = "david.smith@penzle.com",
            FirstName = "Norman",
            LastName = "Doe",
            Sex = Sex.Male,
            DateOfBirth = new DateOnly(year: 1955, month: 06, day: 11, calendar: new GregorianCalendar(type: GregorianCalendarTypes.USEnglish)),
            PostTreatmentTherapy = "Use medical for pain for the next 3 months."
        };

        // Create a new instance of the entry.
        var medicalReleaseId = await managementPenzleClient.Entry.CreateEntry(entry: medicalRelease, cancellationToken: CancellationToken.None);

        // Print the created entry id.
        Console.WriteLine(value: $"The entry id: {medicalReleaseId}");
    }

    public async static Task ExampleHowToUpdateEntry(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });


        var medicalReleaseId = new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"); // The entry id which you want to update.

        // Create a new instance of the entry.
        var medicalRelease = new MedicalRelease
        {
            ParentName = "David Smith",
            EmailAddress = "david.smith@penzle.com",
            FirstName = "Norman",
            LastName = "Doe",
            Sex = Sex.Male,
            DateOfBirth = new DateOnly(year: 1955, month: 06, day: 11, calendar: new GregorianCalendar(type: GregorianCalendarTypes.USEnglish)),
            PostTreatmentTherapy = "Use medical for pain for the next 3 months."
        };

        // Create a new instance of the entry.
        var httpStatusCode = await managementPenzleClient.Entry.UpdateEntry(entryId: medicalReleaseId, entry: medicalRelease, cancellationToken: CancellationToken.None);

        // Print True if the entry was updated successfully.
        Console.WriteLine(httpStatusCode == HttpStatusCode.NoContent);
    }

    public async static Task ExampleHowToDeleteEntry(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var medicalReleaseId = new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"); // The entry id which you want to deleted.

        // Create a new instance of the entry.
        var httpStatusCode = await managementPenzleClient.Entry.DeleteEntry(entryId: medicalReleaseId, cancellationToken: CancellationToken.None);

        // Print True if the entry was deleted successfully.
        Console.WriteLine(httpStatusCode == HttpStatusCode.NoContent);
    }
}
