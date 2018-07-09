using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DscBuildTools.Model
{
    public partial class ClassVersion
    {
        [JsonProperty("build")]
        public string Build { get; set; }

        [JsonProperty("major")]
        public int Major { get; set; }

        [JsonProperty("minor")]
        public int Minor { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }


        public string GetVersion()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Major);
            sb.Append('.');
            sb.Append(Minor);
            sb.Append('.');
            sb.Append(Revision);
            if (int.TryParse(Build, out int value))
            {
                sb.Append('.');
            }
            else
            {
                sb.Append('-');
            }
            sb.Append(Build);
            return sb.ToString();
        }

        public override string ToString() => Serialize.ToJson(this);
    }
}
