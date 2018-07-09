using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace DscBuildTools.Model
{
    [ContentProperty("Properties")]
    public partial class DscResource
    {
        public DscResource()
        {
            ClassVersion = new ClassVersion();
            Properties = new Model.DscResourcePropertyCollection();
        }

        [JsonProperty("classVersion")]
        public ClassVersion ClassVersion { get; set; }

        [JsonProperty("friendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("properties")]
        [JsonConverter(typeof(DscResourcePropertyCollectionConverter))]
        public DscResourcePropertyCollection Properties { get; set; }

        public static DscResource FromJson(string json) => JsonConvert.DeserializeObject<DscResource>(json, Converter.Settings);

        public override string ToString() => Serialize.ToJson(this);

        internal class DscResourcePropertyCollectionConverter : JsonConverter
        {
            private JsonReader _reader;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            private JsonToken CurrentTokenType => _reader.TokenType;


            private object CurrentValue => _reader.Value;

            [DebuggerStepThrough]
            private bool Read() => _reader.Read();

            [DebuggerStepThrough]
            private string ReadString()
            {
                return _reader.ReadAsString();
            }

            [DebuggerStepThrough]
            private int ReadInt()
            {
                return _reader.ReadAsInt32().Value;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(DscResourcePropertyCollection);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                _reader = reader;

                DscResourcePropertyCollection collection = new DscResourcePropertyCollection();

                while (Read())
                {
                    if (CurrentTokenType == JsonToken.StartObject)
                    {
                        DscResourceProperty property = new DscResourceProperty();

                        while (Read())
                        {
                            if (CurrentTokenType == JsonToken.PropertyName)
                            {
                                string name = CurrentValue.ToString();

                                switch (name)
                                {
                                    case "name":
                                        {
                                            property.Name = ReadString();
                                            break;
                                        }
                                    case "description":
                                        {
                                            property.Description = ReadString();
                                            break;
                                        }
                                    case "attribute":
                                        {
                                            string attributeValue = ReadString();
                                            property.Attribute = DscResourceAttributes.Parse(attributeValue);
                                            break;
                                        }
                                    case "type":
                                        {
                                            string typeValue = ReadString();
                                            property.Type = DscResourceTypes.Parse(typeValue);
                                            break;
                                        }
                                    case "possibleValues":
                                        {
                                            List<string> list = new List<string>();
                                            while (Read())
                                            {
                                                if (CurrentTokenType == JsonToken.String)
                                                {
                                                    list.Add(CurrentValue.ToString());
                                                }

                                                if (CurrentTokenType == JsonToken.EndArray) break;
                                            }
                                            property.PossibleValues = list;
                                            break;
                                        }
                                }                               
                            }

                            if (CurrentTokenType == JsonToken.EndObject) break;
                        }

                        collection.Add(property);
                    }

                    if (CurrentTokenType == JsonToken.EndArray) break;
                }
                return collection;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
