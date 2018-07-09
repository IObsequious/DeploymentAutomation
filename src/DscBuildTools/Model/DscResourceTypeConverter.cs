using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DscBuildTools.Model
{
    internal class DscResourceTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DscResourceAttribute);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object currentValue = reader.Value;
            JsonToken currentType = reader.TokenType;

            if (reader.TokenType == JsonToken.String)
            {
                string value = reader.ReadAsString();

                if (Enum.TryParse<DscResourceAttribute>(value, out DscResourceAttribute result))
                {
                    return result;
                }
            }

            throw new ArgumentException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WritePropertyName("attribute");
            writer.WriteValue(value.ToString());
        }
    }
}
