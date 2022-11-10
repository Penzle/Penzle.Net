using Penzle.Net.GettingStarted.ConsoleExample.Examples.Entries;

// For the real world usage, you'd want to use this url from configuration.
const string DefaultUrl = "https://maglaj-penzle-demo-api-app.azurewebsites.net/api";

// For the real world usage, you'd want to use this key from secured configuration such as Azure Key Vault.
const string ApiKey = "eyJ0eXAiOiJhdFx1MDAyQmp3dCIsImFsZyI6IkhTMjU2In0.eyJleHAiOjE2Njk3NjI4MDAsInRva2VuVHlwZSI6ImFwaV90b2tlbiIsImFjY2Vzc190b2tlbl9leHAiOiIyMDIyLTExLTI5VDIzOjAwOjAwLjAwMDAwMDBaIiwibmJmIjoxNjY4MDg3Mjk3LCJpc3MiOiJodHRwczovL21hZ2xhai1wZW56bGUtZGVtby1hcGktYXBwLmF6dXJld2Vic2l0ZXMubmV0IiwiYXVkIjoiaW5maW5pdHkuY21zLmFwaSIsImF1dGhfdGltZSI6MTY2ODA4NzI5Nywic2NvcGUiOiJhcGkud3JpdGUiLCJwcm9qZWN0IjoiMTA4OGVhOTktMTQxMS00ZmI5LWI2OTQtM2M5NzQ1NDRiM2FhIiwiZW52aXJvbm1lbnRzIjpbeyJOYW1lIjoibWFpbiIsIklkIjoiMzQ4Njg0NzQtYzc2Yy00NzY5LWFhMWEtNGZiOTIxOGNkMjc0In1dfQ.ApdO4JKCSV2sJQkjhMqLghG-HQsjKPFC0O4R2z8wZMc";

// Define the Penzle API URL including the username.
var apiAddress = new Uri(uriString: DefaultUrl, uriKind: UriKind.Absolute);

await FormsExamples.ExampleHowToCreateFormEntry(uri: apiAddress, apiKey: ApiKey);
