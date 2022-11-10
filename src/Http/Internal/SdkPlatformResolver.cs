// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Globalization;

namespace Penzle.Core.Http.Internal;

public class SdkPlatformInformation : IPlatformInformation
{
    public string PlatformInformation { get; set; } = "Unknown Platform";

    public string GetPlatformInformation()
    {
        if (!string.IsNullOrWhiteSpace(value: PlatformInformation))
        {
            return PlatformInformation;
        }

        try
        {
            return PlatformInformation = string.Format(provider: CultureInfo.InvariantCulture, format: "{0} {1}; {2}",
                arg0: Environment.OSVersion.Platform, arg1: Environment.OSVersion.Version.ToString(fieldCount: 3),
                arg2: Environment.Is64BitOperatingSystem ? "amd64" : "x86");
        }
        catch
        {
            return PlatformInformation;
        }
    }
}
