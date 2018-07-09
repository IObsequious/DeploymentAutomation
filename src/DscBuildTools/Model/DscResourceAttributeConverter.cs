using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DscBuildTools.Model
{
    internal class DscResourceAttributeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DscResourceAttribute);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.Read();

            object currentValue = reader.Value;
            JsonToken currentType = reader.TokenType;

            if (reader.TokenType == JsonToken.PropertyName)
            {
                if (currentValue.ToString() == "type")
                {
                    if (Enum.TryParse<DscResourceType>(reader.ReadAsString(), out DscResourceType type))
                    {
                        return type.ToString();
                    }

                    throw new InvalidOperationException();
                }

                if (currentValue.ToString() == "attribute")
                {
                    if (Enum.TryParse<DscResourceAttribute>(reader.ReadAsString(), out DscResourceAttribute result))
                    {
                        return result.ToString();
                    }
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
