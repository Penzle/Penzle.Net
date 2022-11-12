## **Templates**

Building the project data structure is one of the first steps in starting a new project. 

Penzle will allow you to make data templates, validations, the assignment of child entries, and the creation of content entries, all of which are essential parts of building a data structure that will represent the strong type object in your application with business rules.

Currently, we provide the ability only to get data schema through the Penzle.NET SKD. But soon, you can probably expect more REST API endpoints and SDKs that will give you more ways to manage templates.

### **Models**

As part of the Penzle.NET SDK, we give you the Template model, which makes it easy for you to use this endpoint. The template is similar to how it responds to concrete data like an entry or form, but it only has avoided values. The primary purpose of this template is to put you in a position to use the template of data structure to render forms alongside defined business in your final application.

```csharp
namespace Penzle.Core.Models;

public class Template
{
    public virtual Guid Id { get; set; }
    public virtual Guid ParentId { get; set; }
    public virtual string Name { get; set; }
    public virtual string Version { get; set; }
    public virtual string Language { get; set; }
    public virtual string AliasPath { get; set; }
    public virtual string Slug { get; set; }
    public virtual DateTime ModifiedAt { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public List<BaseTemplates> Base { get; set; } = new();
    public IDictionary<string, object> Fields { get; set; } = new Dictionary<string, object>();
}
```

### **Get template**

Retrieve a template with a unique template id. You are need to provide a minimum API read key in order to get access to the resource you request.

```csharp
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
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns a single template.                                      |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 404  | The requested resource was not found.                           |
| 500  | There was a communication error with the Penzle API.            |

### **Get template by code name**

Retrieve a template with a unique template code which can find in Penzle CNS. You are need to provide a minimum API read key in order to get access to the resource you request.

```csharp
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
```

##### **Response status code**

| Code | Description                                                     |
| ---- | --------------------------------------------------------------- |
| 200  | Returns a single template.                                      |
| 400  | The request is invalid.                                         |
| 401  | The user is not authenticated to access the requested resource. |
| 403  | The user is not authorized to access the requested resource.    |
| 404  | The requested resource was not found.                           |
| 500  | There was a communication error with the Penzle API.            |

#
> ⚠️ The part of SDK for .NET and the underlying documentation are still in the developing phase, and we are working very hard to finish them as quickly as we possibly can.
