using Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries;

// For the real world usage, you'd want to use this url from configuration.
const string DefaultUrl = "<your-api-url>";

// For the real world usage, you'd want to use this key from secured configuration such as Azure Key Vault.
const string ApiKey = "<your-api-key>";

// Define the Penzle API URL including the username.
var apiAddress = new Uri(uriString: DefaultUrl, uriKind: UriKind.Absolute);

// Forms examples.
await FormExamples.ExampleHowToCreateFormEntry(uri: apiAddress, apiKey: ApiKey);
await FormExamples.ExampleHowToUpdateFormEntry(uri: apiAddress, apiKey: ApiKey);
await FormExamples.ExampleHowToDeleteFormEntry(uri: apiAddress, apiKey: ApiKey);
await FormExamples.ExampleHowToGetFormEntry(uri: apiAddress, apiKey: ApiKey);

// Entries examples.
await EntryExamples.ExampleHowToGetEntryCollection(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToGetEntry(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToCreateEntry(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToUpdateEntry(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToDeleteEntry(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToGetEntryCollectionUsingTemplate(uri: apiAddress, apiKey: ApiKey);
await EntryExamples.ExampleHowToGetEntryBySlug(uri: apiAddress, apiKey: ApiKey);

// Assets examples.
await AssetExamples.ExampleHowToGetAssetCollection(uri: apiAddress, apiKey: ApiKey);
await AssetExamples.ExampleHowToGetAsset(uri: apiAddress, apiKey: ApiKey);
await AssetExamples.ExampleHowToCreateAsset(uri: apiAddress, apiKey: ApiKey);
await AssetExamples.ExampleHowToUpdateAsset(uri: apiAddress, apiKey: ApiKey);
await AssetExamples.ExampleHowToDeleteAsset(uri: apiAddress, apiKey: ApiKey);
