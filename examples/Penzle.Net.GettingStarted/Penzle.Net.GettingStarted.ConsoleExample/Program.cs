using Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries;

// For the real world usage, you'd want to use this url from configuration.
const string DefaultUrl = "https://api-<your-username-or-tenant>.penzle.com";

// For the real world usage, you'd want to use this key from secured configuration such as Azure Key Vault.
const string ApiKey = "<your-api-key>";

// Define the Penzle API URL including the username.
var apiAddress = new Uri(uriString: DefaultUrl, uriKind: UriKind.Absolute);

await EntriesExample.ExampleHowToPullEntryData(uri: apiAddress, apiKey: ApiKey);
