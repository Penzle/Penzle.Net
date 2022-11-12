// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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
