## **Configuration Penzle Client**

Dependency injection is appropriate for ASP.NET Core applications including Web API, Blazor, MVC Application, etc.

You will need to go to your Startup.cs file and add the penzle client to the service registration section. We are
supporting a two-way setup for the binding necessary components. They can be retrieved via appsettings.json or directly
defined using configurator.

### **appsettings.json**

```json
{
  ...

  "PenzleApiConfig": {
    "BaseUri": "httpw://{yourname}.api.penzle.com",
    "ApiDeliveryKey": "eyiOiJ0eXAiOi...",
    "ApiManagementKey": "mJmIjoxNjY1NTc...",
    "Environment": "master",
    "Project": "main",
    "TimeOut": "00:00:30"
  }

  ...
}
```

### **Startup.cs**

```csharp

  //Configuration can be set up using the json source target /or any other source that Microsoft Configuration can read.
var configuration = new ConfigurationBuilder()
        .AddJsonFile(path: "appsettings.json")
        .Build();

serviceCollection
        .AddPenzleClient(configuration: configuration);
```

The SDK obtains the configuration for this scenario from the PenzleApiConfig section of the Configuration object. It is
possible to provide the configuration to the IPenzleClient in a variety of different ways, and there are also a number
of different sophisticated registration scenarios and methods that utilize more secure methods such
as [Microsoft Azure Key Vault](<[https://link](https://azure.microsoft.com/en-us/products/key-vault/#product-overview)>)
.

We support direct configuration through the use of a dynamic configurator as an alternative to the use of configuration
files.

```csharp
 serviceCollection
        .AddPenzleClient(configurator: cfg =>
            {
                cfg.ApiDeliveryKey = "eyiOiJ0eXAiOi...";
                cfg.ApiManagementKey = "mJmIjoxNjY1NTc...";
                cfg.BaseUri = new Uri(uriString: "https://{yourname}.api.penzle.com");
                cfg.Environment = "master";
                cfg.Project = "main";
                cfg.TimeOut = TimeSpan.FromMinutes(value: 1);
            });
```

If you are using the default project and environment, we provide a default instance of ApiOptions for these values. The
method has several overloads where you can pass your own IHttpClient as well as IJsonSerializer serialization.

```csharp
var environment = ApiOptions.Default.Environment; //master
var project = ApiOptions.Default.Project; //main
```

We recommend using the static Factory method derived from PenzleClient if you need to make extensive adjustments or
offer complex configuration options.

```csharp

IPenzleDeliveryClient penzleClient = PenzleDeliveryClient.Factory
     (
        baseAddress: new Uri(uriString: "https://{yourname}.api.penzle.com"),
        apiDeliveryKey: "eyiOiJ0eXAiOi...",
        apiManagementKey: "mJmIjoxNjY1NTc...",
        apiOptions: options =>
        {
            options.Environment = "master";
            options.Environment = "main";
        },
        httpClient: new HttpClientAdapter(getHandler: () => new SocketsHttpHandler()), // IHttpClient
        jsonSerializer: new MicrosoftJsonSerializer() // IJsonSerializer
        );

// Register instance to IoC container.
serviceCollection.AddScoped<IPenzleDeliveryClient>(provider => penzleClient);
```

An `HttpClient` is used internally by the default implementation of `IContentHttpClient`. The `HttpClientFactory` is the
best way to create a new `HttpClient`. Including the `IContentHttpClient` into the `HttpClientFactory` pipeline will
allow you to do this.

Your app's reliability and performance will both improve after you start using the `HttpClientFactory`. In addition, you
may add features on top of it, like `Polly` for `Retry Logic`. Please see the official
Microsoft [documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0)
for details.