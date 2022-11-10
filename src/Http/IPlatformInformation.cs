// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Http;

public interface IPlatformInformation
{
    public string PlatformInformation { get; set; }
    string GetPlatformInformation();
}
