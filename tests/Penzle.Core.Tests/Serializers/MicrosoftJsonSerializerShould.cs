namespace Penzle.Core.Tests.Serializers;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Serializers))]
public class MicrosoftJsonSerializerShould
{
    [Fact]
    public void Construct_MicrosoftJsonSerializer_Which_Is_Sub_Type_Of_IJsonSerializer()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();

        // Act
        var jsonSerializer = serializer.As<IJsonSerializer>();

        // Assert
        jsonSerializer.Should().NotBeNull();
    }

    [Fact]
    public void JsonSerializer_Be_Property_Name_Case_Insensitive()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();

        // Act
        var isPropertyNameCaseInsensitive = serializer.Options.PropertyNameCaseInsensitive;

        // Assert
        isPropertyNameCaseInsensitive.Should().BeTrue();
    }

    [Fact]
    public void JsonSerializer_Reference_Handler_Has_To_Be_Ignore_Cycles()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();

        // Act
        var referenceHandler = serializer.Options.ReferenceHandler;

        // Assert
        referenceHandler.Should().Be(expected: ReferenceHandler.IgnoreCycles);
    }

    [Fact]
    public void JsonSerializer_Not_Be_Indented()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();

        // Act
        var isIndented = serializer.Options.WriteIndented;

        // Assert
        isIndented.Should().BeFalse();
    }

    [Fact]
    public void Ability_To_Serialize_Object_To_Json_String()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();
        var person = new
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Address = new
            {
                Street = "123 Main St", City = "Seattle", State = "WA", PostalCode = "98052"
            }
        };

        // Act
        var json = serializer.Serialize(item: person);

        // Assert
        json.Should().NotBeNullOrWhiteSpace();
        json.Should().Be(expected: "{\"FirstName\":\"John\",\"LastName\":\"Doe\",\"Age\":30,\"Address\":{\"Street\":\"123 Main St\",\"City\":\"Seattle\",\"State\":\"WA\",\"PostalCode\":\"98052\"}}");
    }

    [Fact]
    public void Ability_To_Generic_Deserialize_Json_String_To_Specific_Entity()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();
        const string Json = "{\"FirstName\":\"John\",\"LastName\":\"Doe\",\"Age\":30,\"Address\":{\"Street\":\"123 Main St\",\"City\":\"Seattle\",\"State\":\"WA\",\"PostalCode\":\"98052\"}}";

        // Act
        var person = serializer.Deserialize<Person>(json: Json);

        // Assert
        person.Should().NotBeNull();
        person.Should().BeOfType<Person>();
        person.FirstName.Should().Be(expected: "John");
        person.LastName.Should().Be(expected: "Doe");
        person.Age.Should().Be(expected: 30);
        person.Address.Should().NotBeNull();
        person.Address!.Street.Should().Be(expected: "123 Main St");
        person.Address.City.Should().Be(expected: "Seattle");
        person.Address.State.Should().Be(expected: "WA");
        person.Address.PostalCode.Should().Be(expected: "98052");
    }

    [Fact]
    public void Ability_To_Non_Generic_Deserialize_Json_String_To_Specific_Entity()
    {
        // Arrange
        var serializer = new MicrosoftJsonSerializer();
        const string Json = "{\"FirstName\":\"John\",\"LastName\":\"Doe\",\"Age\":30,\"Address\":{\"Street\":\"123 Main St\",\"City\":\"Seattle\",\"State\":\"WA\",\"PostalCode\":\"98052\"}}";

        // Act
        var entity = serializer.Deserialize(json: Json, returnType: typeof(Person));
        var person = entity as Person;

        // Assert
        person.Should().NotBeNull();
        person.Should().BeOfType<Person>();
        person!.FirstName.Should().Be(expected: "John");
        person.LastName.Should().Be(expected: "Doe");
        person.Age.Should().Be(expected: 30);
        person.Address.Should().NotBeNull();
        person.Address!.Street.Should().Be(expected: "123 Main St");
        person.Address.City.Should().Be(expected: "Seattle");
        person.Address.State.Should().Be(expected: "WA");
        person.Address.PostalCode.Should().Be(expected: "98052");
    }
}
