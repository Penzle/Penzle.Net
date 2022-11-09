# **Penzle Delivery and Management .NET SDK**

![Run Build and Test](https://github.com/Penzle/Penzle.Net/actions/workflows/run-build-and-test.ci.yml/badge.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/edbdt6fl1omedpfi/branch/main?svg=true)](https://ci.appveyor.com/project/admir-live/penzle-net/branch/main)

![NuGet Downloads](<https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads&style=plastic](https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads)>)
![Licence](https://camo.githubusercontent.com/238290f8deb751619ca04ad3d316f1246a498b13d2ab49c0348e2b4311bd08f4/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f6c6963656e73652f6a6f6e6772616365636f782f616e7962616467652e737667)
![W3C](https://img.shields.io/badge/w3c-validated-brightgreen)
![Paradigm](https://img.shields.io/badge/accessibility-yes-brightgreen)

## **Penzle Headless CMS**

#### Our technologies can help you build next generation of your apps

At Penzle, we aimed to develop the best headless CMS system that enables seamless, safe, and efficient API-driven
content management for an exceptional online and mobile experience. Our cloud-based headless CMS permits advanced,
code-free content editing across all devices. The outcome? You are able to save time and generate versatile,
customizable content.

| Asset Management                                                                                   | Form Builder                                                                                                                                | Experience Manager                                                                                                                                | Digital Marketing                                                                                       |
| -------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- |
| Locate digital asset filed quickly with our organized and centralized solution to file management. | Use forms across your website to perform surveys, gather leads, or take registrations. Penzle’s form builder makes building forms a breeze. | See what web visitors see, as you build it. Experience Manager is a user-friendly way for content managers to contribute to the building process. | Leverage insights and personalization capability to execute successful omnichannel marketing campaigns. |

Penzle represent client library that makes it simple to communicate with the Penzle RESTFull API. It was developed with
the intention of targeting versions of the .NET Framework that are greater than or equal to 4.6, as well as versions of
DotNet Core and .NET Standard. Simply going to the [website](https://www.penzle.com) for Penzle Tech Stack will allow
you to acquire further knowledge regarding the product.

## **Getting started**

Installation of the core penzle package without support for dependency injection using the Visual Studio Package Manager
Console:

```
Install-Package Penzle.Net
```

Installation using .NET CLI:

```
dotnet add <your project> package Penzle.Net
```

Installation of the penzle package with support for dependency injection using the Visual Studio Package Manager
Console:

```
Install-Package Penzle.Net.Microsoft.DependencyInjection
```

Installation using .NET CLI:

```
dotnet add <your project> package Penzle.Net.Microsoft.DependencyInjection
```

## **Recommended & minimum configuration example**

Create a strong type model that is compatible with your data template first before you do anything else.

```csharp
public sealed class Author
{
    public string? Summary { get; set; }
}
```

```csharp
public sealed class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
}
```

Our secure architecture served as the foundation for the creation of clients for delivery and management. The key must be different for delivery and managament client. Otherwise, the system will return a forbidden response. Please click the following [link](https://docs.penzle.com/api/authentication-and-authorization/index.html) to learn more about it.

To retrieve data from the Penzle API, you must make a call using the `IDeliveryPenzleClient`.

If you're using our package for IoC, you need to set it up the way recommended in the documentation. If not, you can use the static method `Factory`, which can be accessed through `DeliveryPenzleClient` and has multiple overloads. There is an example of how to set up the minimum configuration.

```csharp

// You should use this url from configuration for actual world usage, such as production, or even just for best practices.
const string DefaultUrl = "https://api-{your-tenant-name}.penzle.com";
var apiAddress = new Uri(uriString: DefaultUrl, uriKind: UriKind.Absolute);

// You should use this key from a secure configuration, such as Azure Key Vault, following the best security practice.
const string ApiKey = "<your-key" > ;

// Use the Factory method to create a new instance of the Penzle API client, giving the API address and API key.
var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: apiAddress, apiDeliveryKey: ApiKey, apiOptions: options =>
{
	options.Project = "default"; // Define the project name which you want to use.
	options.Environment = "main"; // Define the environment name which you want to use for the project.
});

// Using query builder to define request parameters.
var query = QueryEntryBuilder.Instance
            .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
            .WithPagination(pagination:QueryPaginationBuilder.Default
                            .WithPage(page: 1)
                            .WithPageSize(pageSize: 10));

try
{

    // You can call the API methods for fetching the entry data with pagination using an instance of the Penzle API client that you have created.
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
catch (PenzleException exception) // Handle exceptions.
{
	Console.WriteLine(value: exception);
	throw;
}
```

## **Usage system assets and resources using SDK**

- [Delivery and management entries](https://github.com/Penzle/Penzle.Net/blob/main/docs/entries.md)
- [Delivery and management forms - TODO]()
- [Delivery and management assets- TODO]()
- [Delivery and management templates - TODO]()

## **Additional information**

In order to find more developers material please visit next sections:

- [OAuth Flow for Authentication and Authorization.](https://github.com/Penzle/Penzle.Net/blob/main/docs/authenticated-access.md)
- [Increasing modularity through the use of dependency injection.](https://github.com/Penzle/Penzle.Net/blob/main/docs/configuration.md)
- [Utilize HttpClientFactory to improve the overall performance of your application as well as its stability.](https://github.com/Penzle/Penzle.Net/blob/main/docs/http-client-and-penzle-client.md)
- [Utilize models with strong typing to ensure that you get the most out of all of the benefits from string typeping models.](https://github.com/Penzle/Penzle.Net/blob/main/docs/models-with-strong-typing.md)
- [Handling errors in accordance with recommended practices](./docs/status-code-and-errors.md)
- [The recommended procedure for carrying out unit tests.](https://github.com/Penzle/Penzle.Net/blob/main/docs/unit-tests.md)
- [Make sure your keys are safe.](https://github.com/Penzle/Penzle.Net/blob/main/docs/azure-key-vault.md)

## **Contributing to Penzle.Net**

Your thoughts and ideas are much appreciated. If you're interested in helping out with this project in any way, we'd
like to make it as clear and straightforward as possible for you to do so, whether that's by:

- Bug reporting
- Addressing the present codebase
- Offering a patch
- Advancing ideas for brand new capabilities
- Taking on the role of a maintainer

Github is where we host our code, manage bug reports and feature requests, and incorporate changes suggested by our
users.
Report bugs using Github's issues. We host our code on Github, which is also where we manage user bug reports and
feature requests and incorporate modifications made by users. In general, high-quality bug reports consist of the
following components: background information; reproducible steps; an example of the code, if the reporter possesses such
an example.

## **License**

MIT License

Copyright (c) 2022 Penzle LLC

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
