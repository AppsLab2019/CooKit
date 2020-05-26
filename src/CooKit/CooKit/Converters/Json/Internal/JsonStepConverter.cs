using System;
using System.Linq;
using CooKit.Models.Steps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CooKit.Converters.Json.Internal
{
    internal sealed class JsonStepConverter : JsonConverter<IStep>
    {
        public const string TypePropertyName = "Type";

        public override IStep ReadJson(JsonReader reader, Type objectType, IStep existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            if (!obj.TryGetValue(TypePropertyName, out var typeToken))
                return null;
            
            if (typeToken is null)
                return null;

            var type = typeToken.ToObject<StepType>(serializer);

            return type switch
            {
                StepType.Text => obj.ToObject<TextStep>(),
                StepType.Image => obj.ToObject<ImageStep>(),
                _ => null
            };
        }

        public override void WriteJson(JsonWriter writer, IStep value, JsonSerializer serializer)
        {
            var obj = JObject.FromObject(value);

            var type = value switch
            {
                ITextStep _ => StepType.Text,
                IImageStep _ => StepType.Image,
                _ => throw new ArgumentOutOfRangeException()
            };
            var typeToken = JToken.FromObject(type, serializer);

            obj[TypePropertyName] = typeToken;
            obj.WriteTo(writer, serializer.Converters.ToArray());
        }
    }
}