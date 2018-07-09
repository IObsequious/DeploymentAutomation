using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DscBuildTools.Model
{
    public partial class DscResourceProperty
    {
        public DscResourceProperty()
        {
            PossibleValues = new List<string>();
        }

        [JsonProperty("attribute")]
        public DscResourceAttribute Attribute { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public DscResourceType Type { get; set; }

        [JsonProperty("possibleValues")]
        public IList<string> PossibleValues { get; set; }

        internal string GetPossibleValues()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PossibleValues.Count; i++)
            {
                string value = PossibleValues[i];
                sb.Append(value);
                if (i != PossibleValues.Count - 1)
                {
                    sb.Append('^');
                }
            }
            return sb.ToString();
        }

        public override string ToString() => Serialize.ToJson(this);

        internal CodeTypeReference GetCodeDomType()
        {
            return new CodeTypeReference( DscResourceTypes.GetClrType(Type) );
        }

        internal string GetMOFType()
        {
            return Type.ToString().Replace("Array", "[]");
        }

        internal string GetClrType()
        {
            return DscResourceTypes.Convert(Type);
        }
    }
}
