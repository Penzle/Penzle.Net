# **Penzle .NET SDK**

The Penzle .NET SDK is a library that allows developers to easily integrate with the Penzle Content Delivery and Content Management APIs. This SDK is designed for .NET developers who want to create, update and retrieve content from the Penzle platform.

[![Build status](https://ci.appveyor.com/api/projects/status/edbdt6fl1omedpfi/branch/main?svg=true)](https://ci.appveyor.com/project/admir-live/penzle-net/branch/main)

![NuGet Downloads](https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads&style=plastic](https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads))
![Licence](https://camo.githubusercontent.com/238290f8deb751619ca04ad3d316f1246a498b13d2ab49c0348e2b4311bd08f4/68747470733a2f2f696d672e736869656c64732e696f2f6769746875622f6c6963656e73652f6a6f6e6772616365636f782f616e7962616467652e737667)
![W3C](https://img.shields.io/badge/w3c-validated-brightgreen)
![Paradigm](https://img.shields.io/badge/accessibility-yes-brightgreen)

## **Getting started**

To install the Penzle .NET SDK, you can use the following options:

- Run the following command in the Package Manager Console:

```
Install-Package Penzle.Net
```

- Run the following .NET CLI command in the command-line:

```
dotnet add <your project> package Penzle.Net
```

## Usage

To retrieve content from the Penzle APIs, you will use the `DeliveryPenzleClient` class. This class provides methods for retrieving content from the Penzle Delivery API.
You will use `ManagementPenzleClient `class to create, update, and delete content.

You can use a client with DI/IoC or without DI/IoC.

### Without DI/IoC

```csharp
   var client = DeliveryPenzleClient
          .Factory
          (
              apiDeliveryKey: "<delivery_api_key>",
              (api) =>
              {
                  api.Environment = "<environment_name>";
                  api.Project = "<project_name>";
              }
          );
```

### With DI/IoC

```csharp
    public void ConfigureServices(IServiceCollection services)
    {
	    services.AddPenzleClient(Configuration);
    }
```

```csharp
    public class HomeController
    {
	    private IDeliveryPenzleClient client;

	    public HomeController(IDeliveryPenzleClient deliveryPenzleClient)
	    {
		    client = deliveryPenzleClient;
	    }
    }
```

## Retrieving data

After creating a Penzle client, you can retrieve data from Penzle APIs:

```csharp
    // Retrieving a single entry by ID
    var entry = await client.Entry.GetEntry<Author>(<author_id>);
  
    Console.WriteLine(entry.Summary); // => Penzle author.
    Console.WriteLine(entry.Age); // => 27.
  
```

Create a strong type model that is compatible with your data template first before you do anything else.

```csharp
public sealed class Author
{
    public string Summary { get; set; }
    public int Age { get; set; }
}
```

If you need system data of the entry, add an EntrySystem property to the class.

```csharp
public sealed class Author
{
    public EntrySystem System { get; init; }
    public string Summary { get; set; }
    public int Age { get; set; }
}
```

The `GetEntries `return collections of strongly-typed objects, based on the content types you have defined in your Penzle project.
You can use LINQ to filter, sort and transform the content before it is returned.

```csharp

    // Use query builder to define request parameters.
    var query = QueryEntryBuilder.New
                .Where(x => x.Summary.Contains("J") && x.Age >= 30)
                .OrderBy(x => x.Id)
                .Select(x => new { x.Id, x.FirstName });
        
    var entries = await client.Entry.GetEntries<Author>(query: query);

  // Print the entries to the console.
    foreach (var entry in entries)
    {
        // Print the entry system fields.
        Console.WriteLine(value: $"Summary {entry.Summary}");
        Console.WriteLine(value: $"Age: {entry.Age}");
    }
```

> You can find complete examples in the.NET 7 console project, which is written in a "How to" approach for developers. Visit [Penzle.Net.GettingStarted](/examples/Penzle.Net.GettingStarted) to view all examples of how to use various methods in console applications.

## Management API

To create, update or delete data in your Penzle project, you will need to use the Penzle Management API. The Penzle .NET SDK provides a `ManagementPenzleClient` class that you can use to interact with the management API:

```csharp
   var client = ManagementPenzleClient
          .Factory
          (
              apiManagementKey: "<management_api_key>"
          );
```

```csharp

    // Create a new instance of the form entry.
    var author = new Author
    {
        Summary = "David Smith",
        Age = 20,
    };

    // Create a new instance of the entry.
    var medicalReleaseId = await client.Entry.CreateEntry(author, CancellationToken.None);
```

## SDK Documentation

The official SDK documentation provides detailed information about the various classes and methods available in the SDK. Please follow the documentation for more details. 

## License

This SDK is released under the [MIT License](./LICENSE).

## Reach out to us

We welcome feedback, bug reports, and feature requests.

If you need help with this library, please contact the vendor support.

You can also open an issue on the GitHub repository or submit a pull request with improvements to the code.

If you have any questions or suggestions, please feel free to reach out to us by sending an email to support@penzle.com.

We are looking forward to hearing from you!

## Contribution

We welcome contributions to this library. If you are interested in contributing, please read the [CONTRIBUTING](./CONTRIBUTING.md) file for more information on how to get started.
