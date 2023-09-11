using Domain;

namespace Application.Common.Interfaces;

public interface ISerializer
{
    string Serialize(object obj);
    T? Deserialize<T>(string json);
    object? DeserializeTypeAware(TypeAwareSerializedObject content);
}