using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Management.Infrastructure;

namespace DscBuildTools.Model
{
    public static class DscResourceTypes
    {
        public static Type GetClrType(DscResourceType type)
        {
            switch (type)
            {
                case DscResourceType.Boolean: return typeof(bool);
                case DscResourceType.BooleanArray: return typeof(bool[]);
                case DscResourceType.Char: return typeof(char);
                case DscResourceType.CharArray: return typeof(char[]);
                case DscResourceType.DateTime: return typeof(DateTime);
                case DscResourceType.DateTimeArray: return typeof(DateTime[]);
                case DscResourceType.Single: return typeof(Single);
                case DscResourceType.SingleArray: return typeof(Single[]);
                case DscResourceType.Double: return typeof(double);
                case DscResourceType.DoubleArray: return typeof(double[]);
                case DscResourceType.Int16: return typeof(short);
                case DscResourceType.Int16Array: return typeof(short[]);
                case DscResourceType.Int32: return typeof(int);
                case DscResourceType.Int32Array: return typeof(int[]);
                case DscResourceType.Int64: return typeof(long);
                case DscResourceType.Int64Array: return typeof(long[]);
                case DscResourceType.SByte: return typeof(sbyte);
                case DscResourceType.SByteArray: return typeof(sbyte[]);
                case DscResourceType.String: return typeof(String);
                case DscResourceType.StringArray: return typeof(String[]);
                case DscResourceType.UInt16: return typeof(ushort);
                case DscResourceType.UInt16Array: return typeof(ushort[]);
                case DscResourceType.UInt32: return typeof(uint);
                case DscResourceType.UInt32Array: return typeof(uint[]);
                case DscResourceType.UInt64: return typeof(ulong);
                case DscResourceType.UInt64Array: return typeof(ulong[]);
                case DscResourceType.Byte: return typeof(byte);
                case DscResourceType.ByteArray: return typeof(byte[]);
                case DscResourceType.HashTable: return typeof(CimInstance[]);
                case DscResourceType.HashTableArray: return typeof(CimInstance[]);
                case DscResourceType.PSCredential: return typeof(PSCredential);
                case DscResourceType.PSCredentialArray: return typeof(PSCredential[]);
                default: return typeof(string);
            }
        }
        public static DscResourceType Parse(string value)
        {
            switch (value.ToLower())
            {
                case "boolean":
                    return DscResourceType.Boolean;
                case "boolean[]":
                case "booleanarray":
                    return DscResourceType.BooleanArray;
                case "char":
                    return DscResourceType.Char;
                case "char[]":
                case "chararray":
                    return DscResourceType.CharArray;
                case "datetime":
                    return DscResourceType.DateTime;
                case "datetime[]":
                case "datetimearray":
                    return DscResourceType.DateTimeArray;
                case "single":
                    return DscResourceType.Single;
                case "single[]":
                case "singlearray":
                    return DscResourceType.SingleArray;
                case "double":
                    return DscResourceType.Double;
                case "double[]":
                case "doublearray":
                    return DscResourceType.DoubleArray;
                case "int16":
                    return DscResourceType.Int16;
                case "int16[]":
                case "int16array":
                    return DscResourceType.Int16Array;
                case "int32":
                    return DscResourceType.Int32;
                case "int32[]":
                case "int32array":
                    return DscResourceType.Int32Array;
                case "int64":
                    return DscResourceType.Int64;
                case "int64[]":
                case "int64array":
                    return DscResourceType.Int64Array;
                case "sbyte":
                    return DscResourceType.SByte;
                case "sbyte[]":
                case "sbytearray":
                    return DscResourceType.SByteArray;
                case "string":
                    return DscResourceType.String;
                case "string[]":
                case "stringarray":
                    return DscResourceType.StringArray;
                case "uint16":
                    return DscResourceType.UInt16;
                case "uint16[]":
                case "uint16array":
                    return DscResourceType.UInt16Array;
                case "uint32":
                    return DscResourceType.UInt32;
                case "uint32[]":
                case "uint32array":
                    return DscResourceType.UInt32Array;
                case "uint64":
                    return DscResourceType.UInt64;
                case "uint64[]":
                case "uint64array":
                    return DscResourceType.UInt64Array;
                case "byte":
                    return DscResourceType.Byte;
                case "byte[]":
                case "bytearray":
                    return DscResourceType.ByteArray;
                case "hashtable":
                    return DscResourceType.HashTable;
                case "hashtable[]":
                case "hashtablearray":
                    return DscResourceType.HashTableArray;
                case "pscredential":
                    return DscResourceType.PSCredential;
                case "pscredential[]":
                case "pscredentialarray":
                    return DscResourceType.PSCredentialArray;
                default:
                    return DscResourceType.String;
            }
        }
        public static string Convert(DscResourceType type) => TypeMap[type];

        public static Dictionary<DscResourceType, string> TypeMap =
            new Dictionary<DscResourceType, string>
            {
                { DscResourceType.Boolean, DscResourceTypes.Boolean },
                { DscResourceType.BooleanArray, DscResourceTypes.BooleanArray },
                { DscResourceType.Char, DscResourceTypes.Char },
                { DscResourceType.CharArray, DscResourceTypes.CharArray },
                { DscResourceType.DateTime, DscResourceTypes.DateTime },
                { DscResourceType.DateTimeArray, DscResourceTypes.DateTimeArray },
                { DscResourceType.HashTable, DscResourceTypes.CimInstanceArray },
                { DscResourceType.HashTableArray, DscResourceTypes.CimInstanceArray },
                { DscResourceType.PSCredential, DscResourceTypes.PSCredential },
                { DscResourceType.PSCredentialArray, DscResourceTypes.PSCredentialArray },
                { DscResourceType.Single, DscResourceTypes.Single },
                { DscResourceType.SingleArray, DscResourceTypes.SingleArray },
                { DscResourceType.Double, DscResourceTypes.Double },
                { DscResourceType.DoubleArray, DscResourceTypes.DoubleArray },
                { DscResourceType.Int16, DscResourceTypes.Int16 },
                { DscResourceType.Int16Array, DscResourceTypes.Int16Array },
                { DscResourceType.Int32, DscResourceTypes.Int32 },
                { DscResourceType.Int32Array, DscResourceTypes.Int32Array },
                { DscResourceType.Int64, DscResourceTypes.Int64 },
                { DscResourceType.Int64Array, DscResourceTypes.Int64Array },
                { DscResourceType.SByte, DscResourceTypes.SByte },
                { DscResourceType.SByteArray, DscResourceTypes.SByteArray },
                { DscResourceType.String, DscResourceTypes.String },
                { DscResourceType.StringArray, DscResourceTypes.StringArray },
                { DscResourceType.UInt16, DscResourceTypes.UInt16 },
                { DscResourceType.UInt16Array, DscResourceTypes.UInt16Array },
                { DscResourceType.UInt32, DscResourceTypes.UInt32 },
                { DscResourceType.UInt32Array, DscResourceTypes.UInt32Array },
                { DscResourceType.UInt64, DscResourceTypes.UInt64 },
                { DscResourceType.UInt64Array, DscResourceTypes.UInt64Array },
                { DscResourceType.Byte, DscResourceTypes.Byte },
                { DscResourceType.ByteArray, DscResourceTypes.ByteArray },
            };
        
        public const string Boolean = "bool";
        public const string BooleanArray = "bool[]";
        public const string Char = "char";
        public const string CharArray = "char[]";
        public const string DateTime = "DateTime";
        public const string DateTimeArray = "DateTime[]";
        public const string HashTable = "CimInstance[]";
        public const string CimInstanceArray = "CimInstance[]";
        public const string PSCredential = "PSCredential";
        public const string PSCredentialArray = "PSCredential[]";
        public const string Single = "Single";
        public const string SingleArray = "Single[]";
        public const string Double = "double";
        public const string DoubleArray = "double[]";
        public const string Int16 = "short";
        public const string Int16Array = "short[]";
        public const string Int32 = "int";
        public const string Int32Array = "int[]";
        public const string Int64 = "long";
        public const string Int64Array = "long[]";
        public const string SByte = "sbyte";
        public const string SByteArray = "sbyte[]";
        public const string String = "string";
        public const string StringArray = "string[]";
        public const string UInt16 = "ushort";
        public const string UInt16Array = "ushort[]";
        public const string UInt32 = "uint";
        public const string UInt32Array = "uint[]";
        public const string UInt64 = "long";
        public const string UInt64Array = "long[]";
        public const string Byte = "byte";
        public const string ByteArray = "byte[]";

    }
}
