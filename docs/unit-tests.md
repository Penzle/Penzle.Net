## **The recommended procedure for carrying out unit tests.**

If you need to instantiate Response from `IPenzleClient` or your own strongly typed models with custom data, you may
want to inject your own instance of `HttpClient`. This is the case when you don't want to connect to the delivery Penzle
API directly from your unit tests.

To begin, you will need to pretend that you are a member of the `HttpMessageHandler`
class. [StackOverflow](https://stackoverflow.com/questions/22223223/how-to-pass-in-a-mocked-httpclient-in-a-net-test/22264503#22264503)
provides a comprehensive walk through of the process. NuGet package called `RichardSzalay.MockHttp` may be found at the
following
location: [https://www.nuget.org/packages/RichardSzalay.MockHttp](https://www.nuget.org/packages/RichardSzalay.MockHttp)
.

You are able to substitute a phony HttpMessageHandler in place of the real one. Something that has a similar appearance
to this:

```csharp
// Arrange
var mockHttp = new MockHttpMessageHandler();
mockHttp.When("https://{yourname}.api.penzle.com.com*").Respond("application/json", "<desired json>");
```

and then you can create a client that will use the fake handler.

```csharp
var httpClient = mockHttp.ToHttpClient();
```

You can now use the fake `HttpClient` when creating an instance of the `IPenzleClient` interface:

```csharp
IPenzleClient client = DeliveryClientBuilder.Factory(....);
```