## **Entries**

When your content is ready to be delivered, you can use the Penzle SDK to fetch individual content or collections of
content using pagination. This is possible once your content is ready for delivery.

Retrieve a single entry by entry id you can use following example:

```csharp
 var student = await penzleClient
            .Entry.GetEntry<Student>
            (
                entryId: new Guid(g: "b30f6a28-e8b9-4886-ac28-0109aaf959af"),
                cancellationToken: CancellationToken.None
            );
```

Retrieves all of the entries associated with a certain template, with the possibility to filter the results using a
querystring built with `QueryEntryBuilder`. Utilizing the class rather than manually building a query results in more
productivity. See following example:

```csharp
var queryParams = QueryEntryBuilder.Instance
            .WithLanguage("en-US")
            .WithPagination
            (
                QueryPaginationBuilder.Default
                    .WithPage(1)
                    .WithPageSize(10)
            )
            .WithParentId(new Guid("46A4091C-A8BC-460E-A57D-DFDC184D22F9"))
            .WithIds(ids: "e9948b9c-1b73-49a2-8888-21d1e31eccfc;e19445f6-63ff-4517-a124-89ee3f08594c");

var pagedCollection = await penzleClient.Entry.GetEntries<PagedList<Entry<Student>>>(template: nameof(Student), query: queryParams);
```

When you use a `QueryEntryBuilder` instance, you can specify more granular filters, such as the language, page,
pageSize, parent of the target object, and collection of Ids.

The API will attempt to locate a fallback language within the settings and will serve content in this language in the
event that one is available.

In the event that a collection of Ids is supplied, the API will disregard the ParentId parameter and instead make an
effort to search the content using the flat mode rather than the tree mode.

Value for page is `1` and `10` for page size are the defaults. The options passed to the `WithPageSize(int)` method
allow for customization. If you send a larger number here, the resulting increase in traffic and benwith may likely
negatively affect the user experience for final consumers, so please proceed with caution.

### **Response**

The method that you select to be called has an effect on how the response is obtained as a result of that call. In the
event that you request a single entry, you will be given a flat DTO that is created according to your specifications. If
you do request a collection of entries, then the result of flat the object will be wrapped into `PagedLis<T>`, where T
is the DTO. See following example:

```csharp
public class PagedList<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public T[] Items { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

var pagedCollection = await penzleClient.Entry.GetEntries<PagedList<Entry<Student>>>(template: nameof(Student), query: queryParams);

// Loop collection using iteration.
foreach(student in pagedCollection.Items)
{
    ...
}
```