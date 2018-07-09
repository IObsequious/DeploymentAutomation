using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DscBuildTools.Types;
using DscBuildTools.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DscBuildTools.Model
{
    public class MOFWriter
    {
        private IndentedTextWriter _writer;
        private bool _shouldWriteComma;

        public MOFWriter(TextWriter writer)
        {
            if (writer is IndentedTextWriter indentedTextWriter)
            {
                _writer = indentedTextWriter;
            }
            else
            {
                _writer = new IndentedTextWriter(writer, "    ");
            }
        }

        public void PushIndent() => _writer.Indent++;
        public void PopIndent() => _writer.Indent--;

        public void OpenBrace() => _writer.Write("{");

        public void CloseBrace() => _writer.Write("{");

        public void SemiColon() => _writer.Write(";");

        private void Write(string value) => _writer.Write(value);
        private void WriteLine(string value) => _writer.WriteLine(value);

        public void WriteDscResourceInfo(DscResource info)
        {
            WriteStartClass(info.Name, info.FriendlyName, info.ClassVersion);

            foreach(var resourceProperty in info.Properties)
            {
                WriteDscResourceProperty(resourceProperty);
            }

            WriteEndClass();
        }

        public void WriteDscResourceProperty(DscResourceProperty property)
        {
            WriteStartAttribute();
            Write(property.Attribute.ToString());
            if (!string.IsNullOrEmpty(property.Description))
            {
                WriteEndAttribute();
                WriteDescription(property.Description);
            }
            if (property.PossibleValues.Count > 0)
            {
                WriteValueMap(property.ValueMap);
                WriteEndAttribute();
                WriteValues(property.Values);
            }
            WriteFullEndAttribute();
            Write(" ");
            Write(DscResourceTypeConverter.ConvertToString(property.PropertyType));
            Write(" ");
            Write(property.Name);
            SemiColon();
            WriteLine();
        }

        public void WriteStartClass(string className, string classFriendlyName, ClassVersion classVersion)
        {
            WriteClassAttributes(classVersion, classFriendlyName ?? className);
            WriteClassName(className);
            StartBlock();

        }

        public void WriteEndClass()
        {
            EndBlock();
        }

        public void WriteClassAttributes(ClassVersion classVersion, string friendlyName)
        {
            WriteStartAttribute();
            WriteClassVersion($"{classVersion.Major}.{classVersion.Minor}.{classVersion.Build}.{classVersion.Revision}");
            WriteEndAttribute();
            WriteFriendlyName(friendlyName);
            WriteFullEndAttribute();
        }

        public void WriteClassVersion(string version)
        {
            _writer.Write("ClassVersion(\"");
            _writer.Write(version);
            _writer.Write("\")");
        }

        public void WriteFriendlyName(string friendlyName)
        {
            _writer.Write("FriendlyName(\"");
            _writer.Write(friendlyName);
            _writer.Write("\")");
        }

        public void WriteDescription(string friendlyName)
        {
            _writer.Write("Description(\"");
            _writer.Write(friendlyName);
            _writer.Write("\")");
        }

        public void WriteValueMap(string[] valueMaps)
        {
            if (_shouldWriteComma)
                WriteEndAttribute();
            _writer.Write("ValueMap");
            _writer.Write('{');
            for (int i = 0; i < valueMaps.Length; i++)
            {
                string current = valueMaps[i];
                _writer.Write('"');
                _writer.Write(valueMaps);
                _writer.Write('"');
                if (i != valueMaps.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.Write('}');
            _shouldWriteComma = true;
        }

        public void WriteValues(string[] values)
        {
            if (_shouldWriteComma)
                WriteEndAttribute();
            _writer.Write("ValueMap");
            _writer.Write('{');
            for (int i = 0; i < values.Length; i++)
            {
                string current = values[i];
                _writer.Write('"');
                _writer.Write(values);
                _writer.Write('"');
                if (i != values.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.Write('}');
            _shouldWriteComma = true;
        }

        public void WriteStartAttribute()
        {
            _writer.Write("[");
        }

        public void WriteAttribute(string name)
        {
            if (_shouldWriteComma)
                WriteEndAttribute();
            _writer.Write(name);
            _shouldWriteComma = true;
        }


        public void WriteStringArrayAttribute(string name, params string[] value)
        {
            if (_shouldWriteComma)
                WriteEndAttribute();
            _writer.Write(name);
            _writer.Write('{');
            for (int i = 0; i < value.Length; i++)
            {
                string current = value[i];
                _writer.Write('"');
                _writer.Write(value);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.Write('}');
            _shouldWriteComma = true;
        }

        public void WriteEndAttribute()
        {
            _writer.Write(", ");
        }

        public void WriteFullEndAttribute()
        {
            _writer.Write("]");


        }

        public void WriteClassName(string name)
        {
            _writer.WriteLine($"class {name} : OMI_BaseResource");           
        }

        public void StartBlock()
        {
            OpenBrace();
            WriteLine();
            PushIndent();
        }

        public void EndBlock()
        {
            PopIndent();
            CloseBrace();
            SemiColon();
            WriteLine();
        }

        public void WriteProperty(string name, DscResourceType type, bool embedded)
        {
            _writer.Write(" ");
            switch (type)
            {
                case DscResourceType.Boolean:
                case DscResourceType.BooleanArray:
                    _writer.Write("Boolean");
                    break;
                case DscResourceType.Char:
                case DscResourceType.CharArray:
                    _writer.Write("Char16");
                    break;
                case DscResourceType.DateTime:
                case DscResourceType.DateTimeArray:
                    _writer.Write("DateTime");
                    break;
                case DscResourceType.HashTable:
                case DscResourceType.HashTableArray:
                    _writer.Write("MSFT_KeyValuePair");
                    break;
                case DscResourceType.PSCredential:
                case DscResourceType.PSCredentialArray:
                    _writer.Write("MSFT_Credential");
                    break;
                case DscResourceType.Single:
                case DscResourceType.SingleArray:
                    _writer.Write("Real32");
                    break;
                case DscResourceType.Double:
                case DscResourceType.DoubleArray:
                    _writer.Write("Real64");
                    break;
                case DscResourceType.Int16:
                case DscResourceType.Int16Array:
                    _writer.Write("Sint16");
                    break;
                case DscResourceType.Int32:
                case DscResourceType.Int32Array:
                    _writer.Write("Sint32");
                    break;
                case DscResourceType.Int64:
                case DscResourceType.Int64Array:
                    _writer.Write("Sint64");
                    break;
                case DscResourceType.SByte:
                case DscResourceType.SByteArray:
                    _writer.Write("Sint8");
                    break;
                case DscResourceType.String:
                case DscResourceType.StringArray:
                    _writer.Write("String");
                    break;
                case DscResourceType.UInt16:
                case DscResourceType.UInt16Array:
                    _writer.Write("Uint16");
                    break;
                case DscResourceType.UInt32:
                case DscResourceType.UInt32Array:
                    _writer.Write("Uint32");
                    break;
                case DscResourceType.UInt64:
                case DscResourceType.UInt64Array:
                    _writer.Write("Uint64");
                    break;
                case DscResourceType.Byte:
                case DscResourceType.ByteArray:
                    _writer.Write("Uint8");
                    break;

            }
            _writer.Write(" ");
            _writer.Write(name);
            _writer.WriteLine(";");
        }

        public void WriteLine()
        {
            _writer.WriteLine();
        }
    }
}
