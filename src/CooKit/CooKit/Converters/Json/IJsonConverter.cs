using System;

namespace CooKit.Converters.Json
{
    public interface IJsonConverter
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
        object Deserialize(string json, Type type);
    }
}
