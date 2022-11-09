### **Introduction**

To accommodate the use of the SDK, we have included a different types of query parameters for combining the results of any collection endpoint, whether it be for entries, forms, assets, etc.

Penzle.NET SKD supply multiple query builders, each with their own set of options, exist to help with the retrieval of specific object types, such as entries, forms, or assets.

### Entry query builder

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

If you pass an instance without a query specification, default values will be used, such as the root for the parent id, the default language, the absence of ids, and default pagination parameters such as 1 for page and 10 for page size. More about pagination you find in pagination section.

#### Filter by parent

You can find the parent id of a content entry in the CMS tree structure, and you can pass that id using the parent method in order to return collection entries which belongs to specific parent entry. Like in example:

```csharp
...
var query = QueryEntryBuilder.Instance
        .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a")):
...
```

#### Filter by language

The language code represents the international standard code. If your content is multilingual, it is also possible to filter by language. If a default fallback language has been defined, the system will recognize it and return entries in the default language in case if available language. This is optional parameter. Like in example:

```csharp
...
var query = QueryEntryBuilder.Instance
         .WithLanguage("en-US")
...
```

There is the capability to extend languages and regions within the CMS, allowing you to target audience very precisely.
