﻿namespace Penzle.Core.Tests.Attribute;

public class HeadersDataAttribute : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new[]
        {
            new Dictionary<string, string>
            {
                {
                    "Accept", "application/json"
                },
                {
                    "Content-Type", "application/json"
                },
                {
                    "User-Agent", "Penzle.Net"
                }
            }
        };
    }
}
