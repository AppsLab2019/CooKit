using System;
using CooKit.Converters.Json.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CooKit.Converters.Json
{
    public sealed class JsonConverter : IJsonConverter
    {
        private readonly JsonSerializerSettings _settings;

        public JsonConverter()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            _settings.Converters.Add(new JsonStepConverter());
            _settings.Converters.Add(new StringEnumConverter());
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, _settings);
        }
    }
}
