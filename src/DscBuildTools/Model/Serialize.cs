using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Markup;
using Microsoft.Build.Framework.XamlTypes;


[assembly: XmlnsDefinition("http://schemas.microsoft.com/DesiredStateConfiguration", "DscBuildTools.Model")]

namespace DscBuildTools.Model
{
    public static class Serialize
    {
        public static string ToJson(DscResource o) => JsonConvert.SerializeObject(o, Converter.Settings);
        public static string ToJson(DscResourceProperty o) => JsonConvert.SerializeObject(o, Converter.Settings);
        public static string ToJson(ClassVersion o) => JsonConvert.SerializeObject(o, Converter.Settings);
    }
}
