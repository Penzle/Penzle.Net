using System;
using System.Collections.Generic;

namespace Penzle.Core.Models;

[Serializable]
public class ApiErrorDetail
{
    public List<string> Message { get; set; }
    public string Field { get; set; }
}