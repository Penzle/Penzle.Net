namespace Penzle.Core.Http.Internal;

internal class Request : IRequest
{
    public Request()
    {
        Headers = new Dictionary<string, string>();
        Parameters = new Dictionary<string, string>();
        Timeout = TimeSpan.FromMinutes(value: 2);
    }

    public object Body { get; set; }
    public Dictionary<string, string> Headers { get; }
    public HttpMethod Method { get; set; }
    public Dictionary<string, string> Parameters { get; }
    public Uri BaseAddress { get; set; }
    public Uri Endpoint { get; set; }
    public TimeSpan Timeout { get; }
    public string ContentType { get; set; }
}