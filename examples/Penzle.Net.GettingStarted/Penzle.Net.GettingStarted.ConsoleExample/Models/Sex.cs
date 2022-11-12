// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Net.GettingStarted.ConsoleExample.Models;

internal record Sex
{
    public static Sex Male = new(value: "Male");
    public static Sex Female = new(value: "Female");
    public static Sex Unknow = new(value: "N/A");

    private Sex(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public override string ToString()
    {
        return Value;
    }
}
