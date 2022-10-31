namespace Penzle.Core.Http.Internal;

internal class Request : IRequest
{
    public Request()
    {
        Headers = new Dictionary<string, string>();
        Parameters = new Dictionary<string, string>();
        Timeout = TimeSpan.FromMinutes(value: 2);
    }

    public virtual object Body { get; set; }
    public virtual Dictionary<string, string> Headers { get; set; }
    public virtual HttpMethod Method { get; set; }
    public virtual Dictionary<string, string> Parameters { get; set; }
    public virtual Uri BaseAddress { get; set; }
    public virtual Uri Endpoint { get; set; }
    public virtual TimeSpan Timeout { get; set; }
    public virtual string ContentType { get; set; }
}
