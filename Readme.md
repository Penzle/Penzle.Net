# **Penzle Delivery and Management .NET SDK**

![Run Build and Test](https://github.com/Penzle/Penzle.Net/actions/workflows/run-build-and-test.ci.yml/badge.svg)
![NuGet Downloads]([https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads&style=plastic](https://img.shields.io/nuget/dt/Penzle.Net?label=NuGet%20Downloads))
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

Penzle represent client library that makes it simple to communicate with the Penzle RESTFull API. It was developed with the intention of targeting versions of the .NET Framework that are greater than or equal to 4.6, as well as versions of DotNet Core and .NET Standard. Simply going to the [website](https://www.penzle.com) for Penzle Tech Stack will allow you to acquire further knowledge regarding the product.

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

## **Simple usage example**

To begin, create a DTO model that is compatible with your data template.

```csharp
public class Student
{
    public DateTime DateOfBirth { get; set; }
    public decimal NumberOfIndex { get; set; }
    public string Title { get; set; }
    public EntrySystem System { get; set; }
    public List<BaseTemplates> Base { get; set; };
}
```

If you want to retrieve data from the Penzle API, all you have to do is make a call using the `IPenzleClient`:

```csharp
var student = await penzleClient
                .Entry
                .GetEntry<Student>
                (
                     entryId: new Guid(g: "b30f6a28-e8b9-4886-ac28-0109aaf959af"),
                     cancellationToken: CancellationToken.None
                );

Console.WriteLine(student.Title);
Console.WriteLine(student.DateOfBirth);
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

Your thoughts and ideas are much appreciated. If you're interested in helping out with this project in any way, we'd like to make it as clear and straightforward as possible for you to do so, whether that's by:

- Bug reporting
- Addressing the present codebase
- Offering a patch
- Advancing ideas for brand new capabilities
- Taking on the role of a maintainer

Github is where we host our code, manage bug reports and feature requests, and incorporate changes suggested by our users.
Report bugs using Github's issues. We host our code on Github, which is also where we manage user bug reports and feature requests and incorporate modifications made by users. In general, high-quality bug reports consist of the following components: background information; reproducible steps; an example of the code, if the reporter possesses such an example.

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
