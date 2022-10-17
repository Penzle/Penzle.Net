using System;
using System.IO;

namespace Penzle.Core.Models;

public sealed class UpdateAssetRequest
{
    public FileStream Payload { get; set; }
    public string Description { get; set; }
    public Guid Id { get; set; }
    public string Language { get; set; }
}