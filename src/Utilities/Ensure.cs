using System;
using System.Collections.Generic;
using System.Linq;

namespace Penzle.Core.Utilities;

internal static class Guard
{
    public static void ArgumentNotNull(object value, string name)
    {
        if (value != null)
        {
            return;
        }

        throw new ArgumentNullException(paramName: name);
    }

    public static void ArgumentNotNullOrEmptyString(string value, string name)
    {
        ArgumentNotNull(value: value, name: name);
        if (!string.IsNullOrWhiteSpace(value: value))
        {
            return;
        }

        throw new ArgumentException(message: "String cannot be empty", paramName: name);
    }

    public static void GreaterThanZero(TimeSpan value, string name)
    {
        ArgumentNotNull(value: value, name: name);

        if (value.TotalMilliseconds > 0)
        {
            return;
        }

        throw new ArgumentException(message: "Timespan must be greater than zero", paramName: name);
    }

    public static void GreaterThanZero(int value, string name)
    {
        ArgumentNotNull(value: value, name: name);

        if (value > 0)
        {
            return;
        }

        throw new ArgumentException(message: "Value must be greater than zero", paramName: name);
    }

    public static void ArgumentNotNullOrEmptyEnumerable<T>(IEnumerable<T> value, string name)
    {
        ArgumentNotNull(value: value, name: name);
        if (value.Any())
        {
            return;
        }

        throw new ArgumentException(message: "List cannot be empty", paramName: name);
    }
}