### **Introduction**

To accommodate the use of the SDK, we have included a different types of query parameters for combining the results of any collection endpoint, whether it be for entries, forms, assets, etc.

Penzle.NET SKD supply multiple query builders, each with their own set of options, exist to help with the retrieval of specific object types, such as entries, forms, or assets.

For example, for entries types, there is a complex query that is configured with AND operator:

```csharp
var query = QueryEntryBuilder.Instance
        .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
        .WithLanguage("en-US")
        .WithIds("A672BEEB-E887-4F60-9E28-47F7C614DB92;E932D185-9E4B-4B03-B0B2-DEA6D26E6F56")
        .WithPagination(
            pagination: QueryPaginationBuilder.Default
                .WithPage(page: 1)
                .WithPageSize(pageSize: 10));
```

You can find the parent id of a content entry in the CMS tree structure, and you can pass that id using the parent method in order to return collection entries which belongs to specific parent entry. Like in example:

```csharp
...
var query = QueryEntryBuilder.Instance
        .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a")):
...
```
