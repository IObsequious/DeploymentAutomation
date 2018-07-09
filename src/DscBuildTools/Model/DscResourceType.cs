using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DscBuildTools.Model
{
    public enum DscResourceType
    {
        Boolean,
        BooleanArray,
        Char,
        CharArray,
        DateTime,
        DateTimeArray,
        Single,
        SingleArray,
        Double,
        DoubleArray,
        Int16,
        Int16Array,
        Int32,
        Int32Array,
        Int64,
        Int64Array,
        SByte,
        SByteArray,
        String,
        StringArray,
        UInt16,
        UInt16Array,
        UInt32,
        UInt32Array,
        UInt64,
        UInt64Array,
        Byte,
        ByteArray,

        // Embedded Instances
        HashTable,
        HashTableArray,
        PSCredential,
        PSCredentialArray,
    }
}
