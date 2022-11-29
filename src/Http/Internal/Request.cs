namespace Penzle.Core.Http.Internal;

internal class Request : IRequest
{
    public virtual object Body { get; set; }
    public virtual Dictionary<string, string> Headers { get; set; } = new();
    public virtual HttpMethod Method { get; set; }
    public virtual Dictionary<string, string> Parameters { get; set; } = new();
    public virtual Uri BaseAddress { get; set; }
    public virtual Uri Endpoint { get; set; }
    public virtual TimeSpan Timeout { get; set; } = FromMinutes(value: 2);
    public virtual string ContentType { get; set; }
}
