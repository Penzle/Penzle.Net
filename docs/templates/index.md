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
