using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DscBuildTools.Model
{
    internal static class DscResourceAttributes
    {
        public static DscResourceAttribute Parse(string value)
        {
            switch (value.ToLower())
            {
                case "key": return DscResourceAttribute.Key;
                case "read": return DscResourceAttribute.Read;
                case "required": return DscResourceAttribute.Required;
                default: return DscResourceAttribute.Write;
            }
        }
    }
}
