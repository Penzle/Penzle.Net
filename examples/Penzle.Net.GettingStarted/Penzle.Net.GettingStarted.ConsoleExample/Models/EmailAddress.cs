// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Net;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Penzle.Core.Exceptions;
using Penzle.Core.Models;
using Penzle.Core;

namespace Penzle.Net.GettingStarted.ConsoleExample.Models;

internal record EmailAddress
{
    private const string EmailPattern = "@^[\\w!#$%&\'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&\'*+\\-/=?\\^_`{|}~]+)*((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))\\z";

    [JsonConstructor]
    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Null => new(value: "none@none.com");

    public string Value { get; init; }

    public static implicit operator EmailAddress(string value)
    {
        return Regex.IsMatch(input: value, pattern: EmailPattern) ? new EmailAddress(value: value) : Null;
    }

    public static implicit operator string(EmailAddress value)
    {
        return value?.Value ?? "N/A";
    }
}
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
