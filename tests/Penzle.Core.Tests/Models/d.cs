// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using Penzle.Core.Models;

namespace Penzle.Core.Tests.Models;

public class Entry<T> : IEntry<T>
{
    public Guid Id { get; set; }
    public string LanguageCode { get; set; }
    public string Name { get; set; }
    public string Template { get; set; }
    public Guid ParentId { get; set; }
    public T Fields { get; set; }
}

public interface IEntry<T>
{
    T Fields { get; set; }
}

public class TabContainer
{
    public EntrySystem System { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public Link Link { get; set; }
    public string TabBottomSummary { get; set; }
    public bool? DisplayTabVertical { get; set; }
}
