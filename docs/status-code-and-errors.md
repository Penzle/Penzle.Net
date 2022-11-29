## **Error handling**

When developing a software, error handling is critical to ensuring that the your program runs smoothly and effectively.
One common approach to error handling is through the use of exception handling.

The Penzle Client offer creating an error handler that can automatically identify and respond to potential errors in
code. Typically, this will involve checking for specific error messages or conditions in order to trigger the
appropriate response.

Object Penzle Exception enables developers to read error as routines at every level of the codebase, providing vital
feedback on what errors are occurring and why they are occurring so that they can be addressed quickly and efficiently.

Overall, error handling via Penzle Exception can help ensure the highest levels of quality and stability when building
software programs.


```csharp
try 
{
       IDeliveryPenzleClient client = DeliveryPenzleClient.Factory(...);

        var query = QueryEntryBuilder.Instance
            .WithPagination(
                QueryPaginationBuilder
                    .Default
                        .WithPage(1)
                        .WithPageSize(10));
        
        var response = await client.Entry.GetEntries<Entry<Product>>(query);

}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
{
    // Handle 404 Not Found
}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.Forbidden)
{
    // Handle 403 Forbidden
}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.Unauthorized)
{
    // Handle 401 Unauthorized
}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.BadRequest)
{
    // Handle 400 Bad Request
}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.InternalServerError)
{
    // Handle 500 Internal Server Error
}
catch (PenzleException exception) when (exception.StatusCode == HttpStatusCode.ServiceUnavailable)
{
    // Handle 503 Service Unavailable
}
catch (PenzleException exception)
{
    // Examine the overarching message that comes back from the API.
    Console.WriteLine(value: e.Message);

    // Examine the HTTP status code that is returned by the API.
    Console.WriteLine(value: e.StatusCode);

    // Check the more details regarding the mistake, such as if it was a validation error or whether it was caused by faulty requests, etc.
    Console.WriteLine(value: e.ApiError.Detail);

    // To gain a better understanding of what information is missing from the request, it is necessary to enumerate the errors field by field.
    foreach (var apiErrorDetail in e.ApiError.Errors)
    {
        Console.WriteLine(value: apiErrorDetail.Field);
        Console.WriteLine(value: apiErrorDetail.Message);
    }

    // Calling ToString() allows you to obtain a formatted error message in a single string.
    Console.WriteLine(value: e.ToString());
}
```

When determining whether or not an API request was successful, the Penzle platform uses the standard HTTP response codes.

Any code in the `2xx` range denotes a successful transaction. The `4xx` field contains codes that indicate an error that could not be fixed with the provided information (e.g., Bad Request, Not Found, Unauthorized, Forbbiden).

If an error has occurred with the Penzle backend system if the code is in the range `5xx`.

Some of the `4xx` faults that can be handled programmatically include an error code that provides a condensed explanation of the error that was reported (for example, "bad request" or "not found").

#### 200 Status Code (Success)

If the server is sending a 200 status code to the client, everything is functioning properly. This is the optimal status
code, which is displayed when using the Penzle API.

#### 400 Status Code (Unauthorized)

This status code is sent along with an HTTP WWW-Authenticate response header. This header contains information on how
the client can make a second request for the resource after first prompting the user for authentication credentials.
This status code is very similar to the 403 Forbidden status code, with the exception that in circumstances that result
in this status code, access to the resource can be granted after user authentication has been completed.

Exception: **`PenzleException`**

#### 403 Status Code (Forbidden)

The 403 status code is used as an indication to the server, in response to the request that the client has sent to the
server in order to reach a specific page, that the page in question has been restricted or that access to the relevant
page is not permitted. There is a high probability that the problem with these keys is connected to the types of keys
that have been generated for a specific project. Check more details on section
for [authenticated access](./authenticated-access.md).

Exception: **`PenzleException`**

#### 401 Status Code (Not Found)

Users are likely to come across the error-reporting HTTP status code 404 more frequently than any other. This error
message indicates that the resource you requested could not be located on the server you requested. The user is
presented response status with 404 error code whenever the URL of a page undergoes a change or the page to which it is
related is removed. There will not be an exception generated, and the response status will be null.

Exception: **`PenzleException`**

#### 500 Status Code (Server Error)

The 503 status code indicates that the server is experiencing temporary difficulties. The majority of reasons, such as
server maintenance and overload, have led to the servers' temporary inaccessibility. In addition, hacks aiming towards
bandwidth can contribute to this problem. If you encounter any such issues, please contact our support team.

Exception: **`PenzleException`**

#### 503 Status Code (Server Unavailable)

The 503 status code indicates that there are currently temporary issues with the server. A majority of factors,
including server maintenance and overload, have contributed to the temporary inaccessibility of the servers. In
addition, this predicament can be brought on by cyberattacks directed against bandwidth.

Exception: **`PenzleException`**

