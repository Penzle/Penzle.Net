using Penzle.Core;

var client = ManagementPenzleClient
    .Factory
    (
        "eyJ0eXAiOiJhdFx1MDAyQmp3dCIsImFsZyI6IkhTMjU2In0.eyJleHAiOjE3MDEyOTg4MDAsInRva2VuVHlwZSI6ImFwaV90b2tlbiIsImFjY2Vzc190b2tlbl9leHAiOiIyMDIzLTExLTI5VDIzOjAwOjAwLjAwMDAwMDBaIiwibmJmIjoxNjkxNzI4ODE3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjgwMTIiLCJhdWQiOiJpbmZpbml0eS5jbXMuYXBpIiwiYXV0aF90aW1lIjoxNjkxNzI4ODE3LCJzY29wZSI6ImFwaS53cml0ZSIsInByb2plY3QiOiIxMDg4ZWE5OS0xNDExLTRmYjktYjY5NC0zYzk3NDU0NGIzYWEiLCJlbnZpcm9ubWVudHMiOlt7Ik5hbWUiOiJtYWluIiwiSWQiOiIzNDg2ODQ3NC1jNzZjLTQ3NjktYWExYS00ZmI5MjE4Y2QyNzQifV0sInRlbmFudGlkIjoicGVuemxlLTQ4ZTJmMjMxLTllNjktNGVhMC1iYTZkLWMzZDFhNWI5NGQ3NCJ9.xOZls6Eki1oQCjFF7AZ58i-rnQMUDEjeT-0Tyi6Mo6A",
        api =>
        {
            api.Environment = "main";
            api.Project = "default";
        },
        new Uri("http://localhost:8012/")
    );


var result = await client.User.EnrollUser("foxcon@cnn.com", "foxcon@cnn.com", "Fox", "Con", CancellationToken.None);

Console.WriteLine(result);
