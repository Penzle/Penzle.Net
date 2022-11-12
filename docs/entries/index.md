## **Entries**

When your material is ready to be provided, you can use the Penzle.NET SDK to get individual content or content collections using pagination. Once your content is ready for delivery, this is easily possible.

### **Collection of entries**

This API obtains all entries from a template (the template is a required argument), with the possibility to filter the results using a query string. Using the `QueryEntryBuilder` class is more efficient than manually generating a query when leveraging the Penzle.NET SDK as compared to manually constructing a query. To acquire access to a resource, a minimum API reading key is required. This is example in C#.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

// Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
var deliveryPenzleClient = DeliveryPenzleClient.Factory(baseAddress: uri, apiDeliveryKey: apiKey, apiOptions: options =>
{
	options.Project = "main"; // Define the project name which you want to use.
	options.Environment = "default"; // Define the environment name which you want to use for the project.
});

// Using query builder to define request parameters.
var query = QueryEntryBuilder.Instance
        .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
        .WithPagination(pagination: QueryPaginationBuilder.Default
            .WithPage(page: 1)
            .WithPageSize(pageSize: 10));

// Using created instance of the Penzle API client, you can call the API methods for fetching the entry data with pagination.
var entries = await deliveryPenzleClient.Entry.GetPaginationListEntries<Entry<Author>>(query: query);
	
    // Print the total number of entries.
Console.WriteLine(value:  $"Total count of entries: {entries.TotalCount}");
	
    // Print the entries to the console.
	foreach (var entry in entries.Items)
	{
		// Print the entry system fields.
		Console.WriteLine(value:  $"Entry {entry.System.Name} system fields:");
		Console.WriteLine(value:  $"System Id: {entry.System.Id}");
		Console.WriteLine(value:  $"System Template: {entry.System.Template}");
		Console.WriteLine(value:  $"System Language: {entry.System.Language}");
		Console.WriteLine(value:  $"System Version: {entry.System.Version}");
		Console.WriteLine(value:  $"System CreatedAt: {entry.System.CreatedAt}");
		Console.WriteLine(value:  $"System ModifiedAt: {entry.System.ModifiedAt}");
	
    	// Print the entry fields.
		Console.WriteLine(value:  $"Entry {entry.System.Name} fields:");
		Console.WriteLine(value:  $"Summary: {entry.Fields.Summary}");
	
    	// Print the entry base collection fields.
		foreach (var @base in entry.Base)
		{
			Console.WriteLine(value: "Entry base fields:");
			Console.WriteLine(value:  $"Fields dictionary: {@base.Fields}");
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
```

### **Collection of entries using template code**

It is possible to have a different name for the class and another word for the template code. In such a scenario, we suggest using the overload method like the one I've included below.

```csharp
/// <summary>
///     Retrieves all the entries of a template, optionally filtered by a querystring. Utilizing the class is more
///     efficient
///     than manually constructing a query. See <see cref="QueryEntryBuilder" />.
/// </summary>
/// <typeparam name="TEntry">The class into which to serialize the response.</typeparam>
/// <param name="query">The optional querystring to add additional filtering to the query.</param>
/// <param name="cancellationToken">The optional token used to cancel an operation.</param>
/// <param name="template">The template code representing the object's shape from which has been created.</param>
/// <returns>A <see cref="TEntry" /> of items.</returns>
/// <exception cref="PenzleException">There was a communication error with the Penzle AP.</exception>
Task<PagedList<TEntry>> GetPaginationListEntries<TEntry>(string template, QueryEntryBuilder query = null, CancellationToken cancellationToken = default) where TEntry : new();
```

The following is a complete example of how to use it in C#.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

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
    var entries = await deliveryPenzleClient.Entry.GetPaginationListEntries<Entry<Author>>("author", query: query);

    // Print the total number of entries.
}
catch (PenzleException exception)
{
    Console.WriteLine(value: exception);
    throw;
}
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns a collection of entries.                                |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 500  | There was a communication error with the Penzle API.            |

### **Get entry**
This Penzle.NET SDK retrieves a single entry by a unique identifier. You must provide a minimum key for API reading to access a resource.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

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
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns a single entry.                                         |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 404  | The requested resource was not found.                           |
| 500  | There was a communication error with the Penzle API.            |


### **Get entry by slug**

If you need access via slug, Penzle.NET SDK will provide method for that. There is a strategy that can be utilized that will make that possible for you. You must provide a minimum key for API reading to gain access to a resource. This is a requirement. There is a complete example in C#.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

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
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns a single entry.                                         |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 404  | The requested resource was not found.                           |
| 500  | There was a communication error with the Penzle API.            |


### **Create entry**
Before beginning with the creation entry, you must create templates and models according to the instructions in the form section. It must to gave a minimum API write key to acquire access to a resource. Once an entry has been created, SKD will return entry id.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

// Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiKey, apiOptions: options =>
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
var medicalReleaseId = await managementPenzleClient.Entry.CreateEntry(medicalRelease, CancellationToken.None);

// Print the created entry id.
Console.WriteLine(value: $"The entry id: {medicalReleaseId}");
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns the id of the newly created entry.                      |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 500  | There was a communication error with the Penzle API.            |


### **Update entry**

The primary distinction between creation and update operations is whether or not the entry id to be updated must be supplied. To access a resource, a minimum API write key must be provided. Once an entry has been modified, the Penzle.SDK will return HTTP status code 204 - No Content.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

// Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiKey, apiOptions: options =>
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
var httpStatusCode = await managementPenzleClient.Entry.UpdateEntry(medicalReleaseId, medicalRelease, CancellationToken.None);

// Print True if the entry was updated successfully.
Console.WriteLine(httpStatusCode == HttpStatusCode.NoContent);
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 204  | The entry was successfully updated.                             |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 500  | There was a communication error with the Penzle API.            |

### **Delete entry**

Using this API, you can delete an existing entry record by providing its id. To access a resource, you will first be required to give a minimum API write key. Once an entry has been deleted, the Penzle.SDK will return HTTP status code 204 - No Content.

```csharp
using Penzle.Core;
using Penzle.Core.Models;

// Create a new instance of the Penzle API client using Factory method ans passing API address and API key.
var managementPenzleClient = ManagementPenzleClient.Factory(baseAddress: uri, apiKey, apiOptions: options =>
{
    options.Project = "main"; // Define the project name which you want to use.
    options.Environment = "default"; // Define the environment name which you want to use for the project.
});

var medicalReleaseId = new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"); // The entry id which you want to deleted.

// Create a new instance of the entry.
var httpStatusCode = await managementPenzleClient.Entry.DeleteEntry(medicalReleaseId, CancellationToken.None);

// Print True if the entry was deleted successfully.
Console.WriteLine(httpStatusCode == HttpStatusCode.NoContent);
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 204  | The entry was successfully deleted.                             |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 500  | There was a communication error with the Penzle API.            |