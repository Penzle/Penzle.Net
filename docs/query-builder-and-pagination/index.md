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

#### Filter by collection of entries id

The SDK give a possiblities to define specific entries using the WithIds method, which requires the entry ids. Separate them with a semicolon for the sake of simplicity.

```csharp
...
var query = QueryEntryBuilder.Instance
        .WithIds("A672BEEB-E887-4F60-9E28-47F7C614DB92;E932D185-9E4B-4B03-B0B2-DEA6D26E6F56")
...
```

Please note that once ids have been defined, the parent is ignored and the entire tree is searched.

### Form query builder

For the sake simplicity the form builder has the same behaviors and follows the same rules as the entry query builder, so we will keep DRY concept here.

```csharp
var query = QueryFormBuilder.Instance
        .WithParentId(parentId: new Guid(g: "2e2c2146-15b1-41ed-9bca-b77e346f8f0a"))
        .WithLanguage("en-US")
        .WithIds("A672BEEB-E887-4F60-9E28-47F7C614DB92;E932D185-9E4B-4B03-B0B2-DEA6D26E6F56")
        .WithPagination(
            pagination: QueryPaginationBuilder.Default
                .WithPage(page: 1)
                .WithPageSize(pageSize: 10));
```

### Asset query builder

For assets types, there is a complex query that is configured with AND operator:

```csharp
var query = QueryAssetBuilder.Instance
    .WithParentId(new Guid("3069970B-AB6C-4C9D-9429-822FB18E971F"))
    .WithKeyword("my picture")
    .WithLanguage("en-US")
    .WithTag("person")
    .WithMimeType("image/jpeg")
    .WithPagination(pagination: QueryPaginationBuilder.Default
        .WithPage(page: 1)
        .WithPageSize(pageSize: 10));
```

### Filter by parent

You can find the parent id of a asset entry in the media tree structure, and you can pass that id using the parent method in order to return collection assets which belongs to specific parent folder. Like in example:

```csharp
...
var query = QueryAssetBuilder.Instance
        .WithParentId(parentId: new Guid(g: "3069970B-AB6C-4C9D-9429-822FB18E971F")):
...
```

#### Filter by language

The language code represents the international standard code like for entries and forms. If your asset is multilingual, it is also possible to filter by language. If a default fallback language has been defined, the system will recognize it and return assets in the default language in case if available language. This is optional parameter. Like in example:

```csharp
...
var query = QueryAssetBuilder.Instance
        .WithLanguage("en-US")
...
```

There is the capability to extend languages and regions within the CMS, allowing you to target audience very precisely.

#### Filter by keyword

Keywords are the words that describe your topic of asset search. These can be individual words or a phrase. These keywords can be chosen from the sentence you create to define your search topic by tags.

```csharp
var query = QueryAssetBuilder.Instance
    .WithKeyword("my picture");
```

#### Filter by tag

The words that describe the asset are represented by a collection of tags. Artificial intelligence is being used by the Penzle system to recognize the contents of images so that they can be used for search purposes.

```csharp
var query = QueryAssetBuilder.Instance
    .WithTag("person")
```

#### Filter by mime type

Mime type represent the label that is used to identify an asset is known as a MIME type. Penzle relies on it to gain an understanding of how to manage the asset. It accomplishes the same goal for all of the similar file extensions.

```csharp
var query = QueryAssetBuilder.Instance
    .WithMimeType("text/html")
```

### Pagination

The Penzle SDK supports a large majority of API request functionalities for retrieving resources through a collection. You could, for instance, list assets, list entries, and so on and so forth. These list SDK methods all have the same basic structure, in which they take at least two parameters like `page` and `pageSize`, in order to perform pagination and improve user experience in the first place.

Suppose you do not specify either `page` or `pageSize` query parameters. In such a case, you will have access to the first page of this stream, which contains the objects in the items array and the stream metadata.

You have the option to specify both the `page` and the `pageSize`, which allows you to describe the page you are targeting and the number of items you want returned on each page.

The default value for the `page` is `1` and for `pageSize` is `10`.

##### **Parameters**

| Name            | Description                                                                                                     | Default | Schema   |
| --------------- | --------------------------------------------------------------------------------------------------------------- | ------- | -------- |
| PageIndex       | A page number for the current stream returned from the backend.                                                 | 1       | integer  |
| PageSize        | Maximum size any individual stream.                                                                             | 10      | integer  |
| TotalCount      | Number of individual objects that make up the entirety of the superset.                                         | N/A     | integer  |
| TotalPages      | Number indicating the overall number of subsets available within the superset.                                  | N/A     | integer  |
| HasPreviousPage | Returns true if the superset is not empty, PageNumber is less than PageCount, and this is not the first subset. | false   | boolean  |
| HasNextPage     | Returns true if the superset is not empty, PageNumber is less than PageCount, and this is not the last subset.  | false   | boolean  |
| Items           | The type of objects that should be included in the collection.                                                  | N/A     | Array [] |

> ⚠️ An increased page size reduces the number of calls to the API endpoint but doubles the quantity of data provided for each API call. Please keep this in mind when defining pageSize, since it could affect performance and the end-user experience.
