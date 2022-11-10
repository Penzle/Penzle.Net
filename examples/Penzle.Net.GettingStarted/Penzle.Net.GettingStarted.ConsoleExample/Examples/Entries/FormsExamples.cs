// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Globalization;
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
            var formId = await managementPenzleClient.Form.CreateForm(form: medicalRelease);

            // Print the form id to the console.
            Console.WriteLine(value: formId);
        }
    }
}
