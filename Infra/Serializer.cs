using System.Text.Json;
using Application.Common.Interfaces;
using Domain;

namespace Infra;

public class Serializer : ISerializer
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public string Serialize(object obj)
        => JsonSerializer.Serialize(obj);

    public T? Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);

    public object? DeserializeTypeAware(TypeAwareSerializedObject content)
    {
        var type = Type.GetType(content.Type);
        return JsonSerializer.Deserialize(content.Content, type!, _jsonSerializerOptions);
    }
}