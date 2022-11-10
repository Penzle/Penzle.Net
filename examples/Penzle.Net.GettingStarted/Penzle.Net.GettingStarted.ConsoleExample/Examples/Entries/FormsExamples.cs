// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Globalization;
using System.Net;
using Penzle.Core;
using Penzle.Net.GettingStarted.ConsoleExample.Models;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries
{
    internal class FormsExamples
    {
        public static async Task ExampleHowToCreateFormEntry(Uri uri, string apiKey)
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
                ParentName = "John Doe",
                EmailAddress = "john.doe@penzle.com",
                FirstName = "Jane",
                LastName = "Doe",
                Sex = Sex.Male,
                DateOfBirth = new DateOnly(year: 1975, month: 11, day: 29, calendar: new GregorianCalendar(type: GregorianCalendarTypes.USEnglish)),
                PostTreatmentTherapy = "Use ice packs and take pain medication."
            };

            // Using created instance of the Penzle API client, you can call the API methods for creating the form entry.
            var formId = await managementPenzleClient.Form.CreateForm(form: medicalRelease, cancellationToken: CancellationToken.None);

            // Print the form id to the console. In this case is "8CE106F3-1F66-47EF-B9DD-D3EA2B8FD686"
            Console.WriteLine(value: formId);
        }

        public static async Task ExampleHowToUpdateFormEntry(Uri uri, string apiKey)
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
                ParentName = "John Doe",
                EmailAddress = "john.doe@penzle.com",
                FirstName = "Jane",
                LastName = "Doe",
                Sex = Sex.Male,
                DateOfBirth = new DateOnly(year: 1975, month: 11, day: 29, calendar: new GregorianCalendar(type: GregorianCalendarTypes.USEnglish)),
                PostTreatmentTherapy = "Use ice packs and take pain medication."
            };

            var medicalReleaseId = new Guid(g: "8CE106F3-1F66-47EF-B9DD-D3EA2B8FD686");

            // Using created instance of the Penzle API client, you can call the API methods for creating the form entry.
            var httpStatusCode = await managementPenzleClient.Form.UpdateForm(formId: medicalReleaseId, form: medicalRelease, cancellationToken: CancellationToken.None);

            // Print the True. If record has been successfully updated expected http status code is 204.
            Console.WriteLine(value: httpStatusCode == HttpStatusCode.NoContent);
        }

        public static async Task ExampleHowToGetFormEntry(Uri uri, string apiKey)
        {
            // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
            var managementPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
            {
                options.Project = "main"; // Define the project name which you want to use.
                options.Environment = "default"; // Define the environment name which you want to use for the project.
            });

            var medicalReleaseId = new Guid(g: "8CE106F3-1F66-47EF-B9DD-D3EA2B8FD686");

            // Using created instance of the Penzle API client, you can call the API methods for creating the form entry.
            var medicalRelease = await managementPenzleClient.Form.GetForm<MedicalRelease>(formId: medicalReleaseId, language: "en-US", cancellationToken: CancellationToken.None);

            // Print the return object to the console.
            Console.WriteLine(value: medicalRelease.ParentName);
            Console.WriteLine(value: medicalRelease.EmailAddress);
            Console.WriteLine(value: medicalRelease.FirstName);
            Console.WriteLine(value: medicalRelease.LastName);
            Console.WriteLine(value: medicalRelease.DateOfBirth);
            Console.WriteLine(value: medicalRelease.PostTreatmentTherapy);
        }

        public static async Task ExampleHowToDeleteFormEntry(Uri uri, string apiKey)
        {
            // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
            var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiManagementKey: apiKey, apiOptions: options =>
            {
                options.Project = "main"; // Define the project name which you want to use.
                options.Environment = "default"; // Define the environment name which you want to use for the project.
            });

            var medicalReleaseId = new Guid(g: "8CE106F3-1F66-47EF-B9DD-D3EA2B8FD686");

            // Using created instance of the Penzle API client, you can call the API methods for creating the form entry.
            var httpStatusCode = await managementPenzleClient.Form.DeleteForm(formId: medicalReleaseId, cancellationToken: CancellationToken.None);

            // Print the True. If record has been successfully deleted expected http status code is 204.
            Console.WriteLine(value: httpStatusCode == HttpStatusCode.NoContent);
        }
    }
}
