using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Penzle.Core.Utilities;

internal static partial class Extensions
{
    private static readonly Regex OptionalQueryStringRegex = new(pattern: "\\{\\?([^}]+)\\}");

    public static string Repeat(this string @this, int repeatCount)
    {
        if (@this.Length == 1)
        {
            return new string(c: @this[index: 0], count: repeatCount);
        }

        var sb = new StringBuilder(capacity: repeatCount * @this.Length);
        while (repeatCount-- > 0)
        {
            sb.Append(value: @this);
        }

        return sb.ToString();
    }

    internal static Uri FormatUri(this string pattern, params object[] args)
    {
        Guard.ArgumentNotNullOrEmptyString(value: pattern, name: nameof(pattern));
        var url = string.Format(provider: CultureInfo.InvariantCulture, format: pattern, args: args);
        return new Uri(uriString: url, uriKind: UriKind.RelativeOrAbsolute);
    }

    public static string UriEncode(this string input)
    {
        return WebUtility.UrlEncode(value: input);
    }

    public static string ToBase64String(this string input)
    {
        return Convert.ToBase64String(inArray: Encoding.UTF8.GetBytes(s: input));
    }

    public static string FromBase64String(this string encoded)
    {
        var decodedBytes = Convert.FromBase64String(s: encoded);
        return Encoding.UTF8.GetString(bytes: decodedBytes, index: 0, count: decodedBytes.Length);
    }

    public static Uri ExpandUriTemplate(this string template, object values)
    {
        var optionalQueryStringMatch = OptionalQueryStringRegex.Match(input: template);
        if (!optionalQueryStringMatch.Success)
        {
            return new Uri(uriString: template);
        }

        var expansion = string.Empty;
        var parameters = optionalQueryStringMatch.Groups[groupnum: 1].Value.Split(separator: ',').ToList();

        foreach (var parameter in parameters)
        {
            var parameterProperty = values.GetType().GetProperty(name: parameter);
            if (parameterProperty == null)
            {
                continue;
            }

            expansion += string.IsNullOrWhiteSpace(value: expansion) ? "?" : "&";
            expansion += $"{parameter}={Uri.EscapeDataString(stringToEscape: "" + parameterProperty.GetValue(obj: values, index: Array.Empty<object>()))}";
        }

        template = OptionalQueryStringRegex.Replace(input: template, replacement: expansion);
        return new Uri(uriString: template);
    }
}