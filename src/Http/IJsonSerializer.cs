using System.Text.Json;

namespace Penzle.Core.Http;

/// <summary>
///     Offers both a serialization and deserialization service for JSON. enables the use of user-defined serializers.
/// </summary>
public interface IJsonSerializer
{
    /// <summary>
    ///     Provides options to be used with <see cref="JsonSerializer" />.
    /// </summary>
    JsonSerializerOptions Options { get; }

    /// <summary>
    ///     Creates a JSON string representation of an object.
    /// </summary>
    /// <param name="item">The object to serialize.</param>
    /// <returns>JSON string</returns>
    string Serialize(object item);

    /// <summary>
    ///     Deserializes a JSON string to an object of specified type.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>Strongly-typed object.</returns>
    T Deserialize<T>(string json);

    /// <summary>
    ///     This function takes a JSON string and converts it into an object of the type you specify.
    /// </summary>
    /// <param name="returnType">The type of the object to deserialize to.</param>
    /// <param name="json">The JSON string deserialize.</param>
    /// <returns>Strongly-typed object.</returns>
    object Deserialize(string json, Type returnType);
}
