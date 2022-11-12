// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core;

namespace Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries;

internal record TemplateExamples
{
    public async static Task ExampleHowToGetTemplateById(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        var templateId = new Guid("8CE106F3-1F66-47EF-B9DD-D3EA2B8FD686");

        // Using created instance of the Penzle API client, you can call the API methods for getting the template by id.
        var template = await deliveryPenzleClient.Template.GetTemplate(templateId: templateId, cancellationToken: CancellationToken.None);

        // Print the template to the console.
        Console.WriteLine(value: template.Name); // This is template name.
        Console.WriteLine(value: template.Language); // This is template language.
        Console.WriteLine(value: template.Type); // This is template type it can be "Form" or "Entry".
        Console.WriteLine(value: template.Fields); // This is template fields. This is a list of the template fields.
        Console.WriteLine(value: template.CreatedAt); // This is template created date.
        Console.WriteLine(value: template.ModifiedAt); // This is template modified date.
        Console.WriteLine(template.Id); // This is template id.
    }

    public async static Task ExampleHowToGetTemplateByCodeName(Uri uri, string apiKey)
    {
        // Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
        var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
        {
            options.Project = "main"; // Define the project name which you want to use.
            options.Environment = "default"; // Define the environment name which you want to use for the project.
        });

        // This is template code name which you can find in the Penzle UI.
        const string TemplateCodeName = "medical-release";

        // Using created instance of the Penzle API client, you can call the API methods for getting the template by code name.
        var template = await deliveryPenzleClient.Template.GetTemplateByCodeName(codeName: TemplateCodeName, cancellationToken: CancellationToken.None);

        // Print the template to the console.
        Console.WriteLine(value: template.Name); // This is template name.
        Console.WriteLine(value: template.Language); // This is template language.
        Console.WriteLine(value: template.Type); // This is template type it can be "Form" or "Entry".
        Console.WriteLine(value: template.Fields); // This is template fields. This is a list of the template fields.
        Console.WriteLine(value: template.CreatedAt); // This is template created date.
        Console.WriteLine(value: template.ModifiedAt); // This is template modified date.
        Console.WriteLine(template.Id); // This is template id.
    }
}
