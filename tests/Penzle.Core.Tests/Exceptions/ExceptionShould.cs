// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Runtime.Serialization;

namespace Penzle.Core.Tests.Exceptions;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Exceptions))]
public class ExceptionShould
{
    [Fact]
    public void Construct_Penzle_Exception_With_Default_Message()
    {
        // Arrange
        const string Message = "An error occurred with this API request";

        // Act
        var exception = new PenzleException();

        // Assert
        exception.Message.Should().Be(Message);
    }

    [Fact]
    public void Construct_Penzle_Exception_With_Default_Message_And_Inner_Exception()
    {
        // Arrange
        const string Message = "An error occurred with this API request";

        // Act
        var exception = new PenzleException(message: Message, innerException: new ArgumentException("This is an inner exception"));

        // Assert
        exception.Message.Should().Be(Message);
        exception.InnerException.Should().NotBeNull();
        exception.InnerException.Should().BeAssignableTo<ArgumentException>();
    }

    [Fact]
    public void Construct_Penzle_Exception_With_Default_Custom_Message()
    {
        // Arrange
        const string Message = "An error occurred with this API request";

        // Act
        var exception = new PenzleException(Message);

        // Assert
        exception.Message.Should().Be(Message);
    }

    [Fact]
    public void Construct_Penzle_Exception_With_Request()
    {
        // Arrange
        var response = new Response(statusCode: HttpStatusCode.BadRequest, body: "This is bad request.", headers: new Dictionary<string, string>(), contentType: "application/json");

        // Act
        var exception = new PenzleException(response);

        // Assert
        exception.Message.Should().Be("This is bad request.");
        exception.HttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        exception.StatusCode.Should().BeDefined();
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public void Construct_Penzle_Exception_With_SerializationInfo_StreamingContext()
    {
        // Arrange
        var serializationInfo = new SerializationInfo(type: typeof(PenzleException), converter: new FormatterConverter());
        var context = new StreamingContext(StreamingContextStates.All);
        var response = new Response(statusCode: HttpStatusCode.BadRequest, body: "This is bad request.", headers: new Dictionary<string, string>(), contentType: "application/json");

        // Act
        var exception = new PenzleException(response);
        exception.GetObjectData(info: serializationInfo, context: context);

        // Assert
        serializationInfo.GetInt32("StatusCode").Should().Be((int)HttpStatusCode.BadRequest);
        serializationInfo.GetValue(name: "HttpResponse", type: typeof(Response)).Should().Be(response);
        serializationInfo.GetValue(name: "ApiError", type: typeof(ApiError)).Should().NotBeNull().And.Subject.As<ApiError>().Title.Should().Be("This is bad request.");
    }

    [Fact]
    public void Construct_Penzle_Exception_With_Exception_Message()
    {
        // Arrange
        var exception = PenzleException.GetApiErrorFromExceptionMessage("This is bad request.");

        // Assert
        exception.Title.Should().Be("This is bad request.");
    }
    
    [Fact]
    public void Construct_Penzle_Exception_With_Request_And_Format_Error_Message_Using_ToString()
    {
        // Arrange
        var response = new Response(statusCode: HttpStatusCode.BadRequest, body: "This is bad request.", headers: new Dictionary<string, string>(), contentType: "application/json");

        // Act
        var exception = new PenzleException(response);

        // Assert
        exception.ToString().Should().Contain("BadRequest - 400 :: This is bad request.");
    }
}
