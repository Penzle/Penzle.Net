using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using Penzle.Core.Http;
using Penzle.Core.Models;
using Penzle.Core.Utilities;

namespace Penzle.Core.Exceptions;

/// <summary>
///     Represents errors that occur when the Penzle APIs are called.
/// </summary>
[Serializable]
public class PenzleException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PenzleException" /> class.
    /// </summary>
    /// <param name="response">The http response of the exception.</param>
    public PenzleException(IResponse response)
    {
        Guard.ArgumentNotNull(value: response, name: nameof(response));

        StatusCode = response.StatusCode;
        ApiError = GetApiErrorFromExceptionMessage(response: response);
        HttpResponse = response;
    }

    internal PenzleException()
    {
    }

    internal PenzleException(string message) : base(message: message)
    {
    }

    internal PenzleException(string message, Exception innerException) : base(message: message, innerException: innerException)
    {
    }

    /// <summary>
    ///     The response from http request.
    /// </summary>
    public IResponse HttpResponse { get; private set; }

    /// <summary>
    ///     The error general message.
    /// </summary>
    public override string Message => ApiErrorMessageSafe ?? "An error occurred with this API request";

    /// <summary>
    ///     The http status code of the exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; private set; }

    /// <summary>
    ///     The details of the exception.
    /// </summary>
    public ApiError ApiError { get; private set; }

    protected string ApiErrorMessageSafe => ApiError != null && !string.IsNullOrWhiteSpace(value: ApiError.Title) ? ApiError.Title : null;

    private static ApiError GetApiErrorFromExceptionMessage(IResponse response)
    {
        var responseBody = response?.Body as string;
        return GetApiErrorFromExceptionMessage(responseContent: responseBody);
    }

    private static ApiError GetApiErrorFromExceptionMessage(string responseContent)
    {
        var error = new ApiError(title: responseContent);
        try
        {
            var json = JsonNode.Parse(json: responseContent);
            if (json != null)
            {
                error = new ApiError(jsonNode: json.AsObject());
            }
        }
        catch (Exception)
        {
            return error;
        }

        return error;
    }

    /// <summary>
    ///     Get formatted error message including error detailed messages.
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();

        var title = $"{StatusCode} - {StatusCode.ToInt32OrDefault()} :: {Message}";
        sb.AppendLine(value: title);

        if (!string.IsNullOrWhiteSpace(value: ApiError.Detail))
        {
            sb.AppendLine(value: $"Detail: '{ApiError.Detail}':");
        }

        if (!string.IsNullOrWhiteSpace(value: ApiError.Instance))
        {
            sb.AppendLine(value: $"Url: '{ApiError.Instance}':");
        }

        if (!ApiError.Errors.Any())
        {
            return sb.ToString();
        }

        foreach (var apiErrorDetail in ApiError.Errors)
        {
            var errors = string.Join(separator: Environment.NewLine, values: apiErrorDetail.Message);
            sb.AppendLine(value: "-".Repeat(repeatCount: title.Length));
            sb.AppendLine(value: $"{apiErrorDetail.Field}:");
            sb.AppendLine(value: errors);
        }

        return sb.ToString();
    }
}