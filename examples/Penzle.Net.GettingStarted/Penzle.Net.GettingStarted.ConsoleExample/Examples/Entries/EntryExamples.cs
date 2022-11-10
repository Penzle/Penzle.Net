﻿// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;
using Penzle.Core.Utilities;
using Penzle.Net.GettingStarted.ConsoleExample.Models;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries
{
    internal record EntryExamples
    {
        public static async Task ExampleHowToGetEntryCollection(Uri uri, string apiKey)
        {
            // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
            var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
            {
                options.Project = "<your-project>"; // Define the project name which you want to use.
                options.Environment = "<your-project-environment>"; // Define the environment name which you want to use for the project.
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

        public static async Task ExampleHowToCreateEntry(Uri uri, string apiKey)
        {
            throw new NotImplementedException();
        }

        public static async Task ExampleHowToUpdateEntry(Uri uri, string apiKey)
        {
            throw new NotImplementedException();
        }

        public static async Task ExampleHowToDeleteEntry(Uri uri, string apiKey)
        {
            throw new NotImplementedException();
        }

        public static async Task ExampleHowToGetEntry(Uri uri, string apiKey)
        {
            throw new NotImplementedException();
        }
    }
}
