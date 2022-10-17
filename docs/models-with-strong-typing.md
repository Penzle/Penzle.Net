## **Models with strong typing**

The fetching of models that have a strong type signature is supported via the `IPenzeClient` interface.

```csharp
var student = await penzleClient.Entry.GetEntry<Student>(entryId: new Guid(g: "b30f6a28-e8b9-4886-ac28-0109aaf959af"), cancellationToken: CancellationToken.None);

Console.WriteLine(student.Title);
Console.WriteLine(student.DateOfBirth);
```

There are many reasons why this method is useful, including:

- type safety when the program is being compiled
- student.DateOfBirth offers a level of usability that is convenient for developers.
- support for functions that depend on the type being used, such as display templates in MVC, Blazor, and so forth.
